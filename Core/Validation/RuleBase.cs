namespace Codell.Pies.Core.Validation
{
    public abstract class RuleBase<T> : IRule<T>
    {
        public abstract bool IsValid(T entity);

        public bool StopValidatingIfBroken { get; set; }

        public bool IsValid(object entity)
        {
            return IsValid((T) entity);
        }

        public abstract string ErrorMessage { get; }

        public IRule<T> And(IRule<T> rightSide)
        {
            return new AndRule<T>(this, rightSide);
        }

        public IRule<T> Or(IRule<T> rightSide)
        {
            return new OrRule<T>(this, rightSide);
        }
    }
}