#if UNITY_EDITOR
using System;

namespace DynamicObjects
{
    public readonly struct EventDefinition
    {
        public readonly EventName name;

        public readonly Type type;

        private readonly string text;

        public EventDefinition(EventName name, Type type)
        {
            this.name = name;
            this.type = type;

            if (type == null)
            {
                this.text = $"{this.name}()";
            }
            else
            {
                this.text = $"{this.name}({TypeUtils.PrettyName(this.type)})";
            }
        }

        public override string ToString()
        {
            return this.text;
        }

        public bool Equals(EventDefinition other)
        {
            return name == other.name && type == other.type;
        }

        public override bool Equals(object obj)
        {
            return obj is EventDefinition other && Equals(other);
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