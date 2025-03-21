namespace Task.DataAccess.IRepositories
{
    public interface IUnitOfWork: IDisposable
    {

        Task<int> CommitAsync();
    }
}
