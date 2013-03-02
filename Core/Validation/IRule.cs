namespace Codell.Pies.Core.Validation
{
    public interface IRule
    {
        bool IsValid(object entity);
        string ErrorMessage { get; }
        bool StopValidatingIfBroken { get; set; }        
    }

    public interface IRule<T> : IRule
    {
        bool IsValid(T entity);
    }
}