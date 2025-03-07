using System;

namespace Model
{
    public class StringWrapper : IEquatable<StringWrapper>
    {
        public string Value { get; set; }

        public StringWrapper(string value)
        {
            Value = value;
        }

        public StringWrapper() { }

        public bool Equals(StringWrapper other)
        {
            if (other == null) return false;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
