namespace Codell.Pies.Web.Extensions
{
    public class IgnoreHtmlFieldPrefix
    {
        public static IgnoreHtmlFieldPrefix Yes = new IgnoreHtmlFieldPrefix(true);
        public static IgnoreHtmlFieldPrefix No = new IgnoreHtmlFieldPrefix(false);
        private readonly bool _value;

        private IgnoreHtmlFieldPrefix(bool value)
        {
            _value = value;
        }

        public static implicit operator bool(IgnoreHtmlFieldPrefix ignore)
        {
            return ignore != null && ignore._value;
        }

        public static implicit operator IgnoreHtmlFieldPrefix(bool value)
        {
            return value ? Yes : No;
        }
    }
}