using System;

namespace DynamicObjects
{
    public readonly struct EventKey
    {
        private readonly EventName name;
        
        private readonly Type type;

        public EventKey(EventName name, Type type)
        {
            this.name = name;
            this.type = type;
        }
        
        public bool Equals(EventKey other)
        {
            return name == other.name && type == other.type;
        }

        public override bool Equals(object obj)
        {
            return obj is EventKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) name * 397) ^ (type != null ? type.GetHashCode() : 0);
            }
        }
    }
}