using System;
using System.Collections.Generic;
using DynamicObjects;

namespace DynamicObjects.Unity
{
    public sealed class ObjectInfo
    {
        private readonly HashSet<PropertyDefinition> propertyDefinitions;

        private readonly HashSet<MethodDefinition> methodDefinitions;

        private readonly HashSet<EventDefinition> eventDefinitions;

        public ObjectInfo()
        {
            this.propertyDefinitions = new HashSet<PropertyDefinition>();
            this.methodDefinitions = new HashSet<MethodDefinition>();
            this.eventDefinitions = new HashSet<EventDefinition>();
        }

        public IEnumerable<PropertyDefinition> GetPropertyDefinitions()
        {
            return this.propertyDefinitions;
        }

        public IEnumerable<MethodDefinition> GetMethodDefinitions()
        {
            return this.methodDefinitions;
        }

        public IEnumerable<EventDefinition> GetEventDefinitions()
        {
            return this.eventDefinitions;
        }

        ///Properties 
        internal void AddProperty<T>(string name)
        {
            var definition = new PropertyDefinition(name, typeof(T));
            this.propertyDefinitions.Add(definition);
        }

        internal void RemoveProperty<T>(string name)
        {
            var definition = new PropertyDefinition(name, typeof(T));
            this.propertyDefinitions.Remove(definition);
        }

        ///Methods 
        internal void AddMethod1(string name)
        {
            var definition = MethodDefinition.Create1(name);
            this.methodDefinitions.Add(definition);
        }

        internal void AddMethod2<T>(string name)
        {
            var definition = MethodDefinition.Create2<T>(name);
            this.methodDefinitions.Add(definition);
        }

        internal void AddMethod3<R>(string name)
        {
            var definition = MethodDefinition.Create3<R>(name);
            this.methodDefinitions.Add(definition);
        }
        
        internal void AddMethod4<T, R>(string name)
        {
            var definition = MethodDefinition.Create4<T, R>(name);
            this.methodDefinitions.Add(definition);
        }
        
        internal void RemoveMethod1(string name)
        {
            var definition = MethodDefinition.Create1(name);
            this.methodDefinitions.Remove(definition);
        }

        internal void RemoveMethod2<T>(string name)
        {
            var definition = MethodDefinition.Create2<T>(name);
            this.methodDefinitions.Remove(definition);
        }

        internal void RemoveMethod3<R>(string name)
        {
            var definition = MethodDefinition.Create3<R>(name);
            this.methodDefinitions.Remove(definition);
        }

        internal void RemoveMethod4<T, R>(string name)
        {
            var defintion = MethodDefinition.Create4<T, R>(name);
            this.methodDefinitions.Remove(defintion);
        }

        internal void AddEvent(string name)
        {
            var definition = new EventDefinition(name, null);
            this.eventDefinitions.Add(definition);
        }

        internal void AddEvent<T>(string name)
        {
            var definition = new EventDefinition(name, typeof(T));
            this.eventDefinitions.Add(definition);
        }

        internal void RemoveEvent(string name)
        {
            var definition = new EventDefinition(name, null);
            this.eventDefinitions.Remove(definition);
        }

        internal void RemoveEvent<T>(string name)
        {
            var definition = new EventDefinition(name, typeof(T));
            this.eventDefinitions.Remove(definition);
        }
        
        internal void Clear()
        {
            this.propertyDefinitions.Clear();
            this.methodDefinitions.Clear();
            this.eventDefinitions.Clear();
        }

        ///Definitions 
        public readonly struct PropertyDefinition
        {
            public readonly string name;

            public readonly Type type;

            private readonly string text;

            public PropertyDefinition(string name, Type type)
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
                return name == other.name && type == other.type && text == other.text;
            }

            public override bool Equals(object obj)
            {
                return obj is PropertyDefinition other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = name != null ? name.GetHashCode() : 0;
                    hashCode = (hashCode * 397) ^ (type != null ? type.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (text != null ? text.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public readonly struct MethodDefinition
        {
            public readonly string name;

            private readonly string text;

            private MethodDefinition(string name, string text)
            {
                this.name = name;
                this.text = text;
            }

            public override string ToString()
            {
                return this.text;
            }

            public bool Equals(MethodDefinition other)
            {
                return name == other.name && text == other.text;
            }

            public override bool Equals(object obj)
            {
                return obj is MethodDefinition other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((name != null ? name.GetHashCode() : 0) * 397) ^ (text != null ? text.GetHashCode() : 0);
                }
            }

            public static MethodDefinition Create1(string name)
            {
                var text = $"{name}()";
                return new MethodDefinition(name, text);
            }

            public static MethodDefinition Create2<T>(string name)
            {
                var type = typeof(T);
                var text = $"{name}({TypeUtils.PrettyName(type)})";
                return new MethodDefinition(name, text);
            }

            public static MethodDefinition Create3<R>(string name)
            {
                var type = typeof(R);
                var text = $"{name}() : {TypeUtils.PrettyName(type)}";
                return new MethodDefinition(name, text);
            }

            public static MethodDefinition Create4<T, R>(string name)
            {
                var paramType = typeof(T);
                var resultType = typeof(R);
                var text = $"{name}({TypeUtils.PrettyName(paramType)}) : {TypeUtils.PrettyName(resultType)}";
                return new MethodDefinition(name, text);
            }
        }
        
        public readonly struct EventDefinition
        {
            public readonly string name;

            public readonly Type type;

            private readonly string text;

            public EventDefinition(string name, Type type)
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
                    var hashCode = (name != null ? name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (type != null ? type.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (text != null ? text.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }
    }
}