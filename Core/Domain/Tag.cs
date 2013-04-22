using Codell.Pies.Common;

namespace Codell.Pies.Core.Domain
{
    public class Tag
    {
        private string Value { get; set; }

        public Tag(string value)
        {
            Verify.NotWhitespace(value, "value");
            Value =  value.ToLower().Trim();
        }

        public static implicit operator string(Tag tag)
        {
            return tag == null ? null : tag.Value;
        }

        public static implicit operator Tag(string value)
        {
            return value.IsEmpty() ? null : new Tag(value);
        }

        public override string ToString()
        {
            return Value;
        }

        public bool Equals(Tag other)
        {
            if (ReferenceEquals(null, other)) return false;
            return ReferenceEquals(this, other) || Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof(Tag) && Equals((Tag)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }         
    }
}