namespace Codell.Pies.Core.Validation
{
    public interface IErrorProvider<T>
    {
        string GetErrorMessageFor(T entity);
    }
}