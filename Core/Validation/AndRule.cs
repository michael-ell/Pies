namespace Codell.Pies.Core.Validation
{
    public class AndRule<T> : IRule<T>
    {
        private readonly IRule<T> _leftSide;
        private readonly IRule<T> _rightSide;

        private string _errorMessage = "";

        public AndRule(IRule<T> leftSide, IRule<T> rightSide)
        {
            _leftSide = leftSide;
            _rightSide = rightSide;
        }

        public IRule<T> LeftSide
        {
            get { return _leftSide; }
        }

        public IRule<T> RightSide
        {
            get { return _rightSide; }
        }

        public bool IsValid(T entity)
        {
            var isLeftValid = _leftSide.IsValid(entity);
            var isRightValid = _rightSide.IsValid(entity);

            if (isLeftValid == false)
                _errorMessage = _leftSide.ErrorMessage;
            else if (isRightValid == false)
                _errorMessage = _rightSide.ErrorMessage;

            return isLeftValid && isRightValid;
        }

        public bool IsValid(object entity)
        {
            return IsValid((T) entity);
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public bool StopValidatingIfBroken { get; set; }
    }
}