using BMG.Core.Messages.Integrations;
using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;
namespace BMG.MessageBus
{

    public class MessageBus : IMessageBus, IDisposable
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;
        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => _bus?.Advanced?.IsConnected ?? false;
        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        public async Task EnqueueAsync<T>(string queueName, T message) where T : IntegrationEvent
        {
            TryConnect();
            await _bus.SendReceive.SendAsync(queueName, message);
        }

        public void Consumer<T>(string queueName, Func<T, Task> onMessage) where T : IntegrationEvent
        {
            TryConnect();
            _bus.SendReceive.Receive<T>(queueName, async msg => await onMessage(msg));
        }



        private void TryConnect()
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString, cfg => cfg.EnableSystemTextJson());
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}
