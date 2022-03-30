#if UNITY_EDITOR
using System;

namespace DynamicObjects
{
    public readonly struct PropertyDefinition
    {
        public readonly PropertyName name;

        public readonly Type type;

        private readonly string text;

        public PropertyDefinition(PropertyName name, Type type)
        {
            this.name = name;
            this.type = type;
            this.text = $"{this.name} : {TypeUtils.PrettyName(type)}";
        }

        public override string ToString()
        {
            return this.text;
        }

        public bool Equals(PropertyDefinition other)
        {
            return name == other.name && type == other.type;
        }

        public override bool Equals(object obj)
        {
            return obj is PropertyDefinition other && Equals(other);
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
#endif