using Codell.Pies.Common;

namespace Codell.Pies.Core.Validation
{
    public class OrRule<T> : IRule<T>
    {
        private readonly IRule<T> _leftSide;
        private readonly IRule<T> _rightSide;

        private string _errorMessage = "";

        public OrRule(IRule<T> leftSide, IRule<T> rightSide)
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

            if (isLeftValid == false && isRightValid == false)
            {
                _errorMessage = string.Format(Resources.OrRuleMessage, _leftSide.ErrorMessage, _rightSide.ErrorMessage);
            }

            return isLeftValid || isRightValid;
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