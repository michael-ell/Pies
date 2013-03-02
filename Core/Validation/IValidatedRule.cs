namespace Codell.Pies.Core.Validation
{
    public interface IValidatedRule<T>
    {
        T Entity { get; }
        bool IsValid { get; }
        string ErrorMessage { get; }        
    }
}