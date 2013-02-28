namespace Codell.Pies.Core.Repositories
{
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void Rollback();
    }
}