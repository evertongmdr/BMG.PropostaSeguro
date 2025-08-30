namespace BMG.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
