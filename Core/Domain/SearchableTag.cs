using Codell.Pies.Common;

namespace Codell.Pies.Core.Domain
{
    public class SearchableTag
    {
        public string Value { get; private set; }

        public SearchableTag(string value)
        {
            Value =  (value ?? "").ToLower().Trim();
            var index = Value.Length - 1;
            var last = index > 0 ? Value[index] : '~';
            if (last == ',' || last == ';')
            {
                Value = Value.Remove(index, 1);
            }
        }

        public static implicit operator string(SearchableTag tag)
        {
            return tag == null ? null : tag.Value;
        }

        public static implicit operator SearchableTag(string value)
        {
            return value.IsEmpty() ? null : new SearchableTag(value);
        }

        public override string ToString()
        {
            return Value;
        }

        public bool Equals(SearchableTag other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(SearchableTag)) return false;
            return Equals((SearchableTag)obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }         
    }
}