using BMG.Core.Data;

namespace BMG.Core.Notifications
{
    public abstract class NotifiableService
    {
        protected readonly NotificationContext _notificationContext;

        public NotifiableService(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        protected async Task<bool> PersistirDados(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                _notificationContext.AddNotification(new Notification("Houve um erro ao persistir os dados"));
                return false;
            }
            return true;

        }
    }

}
