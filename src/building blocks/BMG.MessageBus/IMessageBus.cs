using BMG.Core.Messages.Integrations;
using EasyNetQ;

namespace BMG.MessageBus
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }

        Task EnqueueAsync<T>(string queueName, T message) where T : IntegrationEvent;

        void Consumer<T>(string queueName, Func<T, Task> onMessage) where T : IntegrationEvent;

    }
}
