using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal sealed class MethodBus
    {
        private readonly Dictionary<string, Action> methodMap1;

        private readonly Dictionary<Key1, object> methodMap2;

        private readonly Dictionary<Key1, object> methodMap3;

        private readonly Dictionary<Key2, object> methodMap4;

        internal MethodBus()
        {
            this.methodMap1 = new Dictionary<string, Action>();
            this.methodMap2 = new Dictionary<Key1, object>();
            this.methodMap3 = new Dictionary<Key1, object>();
            this.methodMap4 = new Dictionary<Key2, object>();
        }

        internal void CallMethod(string name)
        {
            if (this.methodMap1.TryGetValue(name, out var provider))
            {
                provider.Invoke();
            }
            else
            {
                throw new Exception($"Method {name}() is not found!");
            }
        }

        internal void CallMethod<T>(string name, T data)
        {
            var key = new Key1(name, typeof(T));
            if (!this.methodMap2.TryGetValue(key, out var action))
            {
                throw new Exception($"Method {name}({typeof(T).Name}) is not found!");
            }

            var provider = (Action<T>) action;
            provider.Invoke(data);
        }

        internal R CallMethod<R>(string name)
        {
            var key = new Key1(name, typeof(R));
            if (!this.methodMap3.TryGetValue(key, out var function))
            {
                throw new Exception($"Method {name}() : {typeof(R).Name} is not found!");
            }

            var provider = (Func<R>) function;
            return provider.Invoke();
        }

        internal R CallMethod<T, R>(string name, T data)
        {
            var key = new Key2(name, typeof(T), typeof(R));
            if (!this.methodMap4.TryGetValue(key, out var function))
            {
                throw new Exception($"Method {name}({typeof(T).Name}) : {typeof(R).Name} is not found!");
            }

            var provider = (Func<T, R>) function;
            return provider.Invoke(data);
        }

        internal bool TryGetDelegate(string name, out Action provider)
        {
            return this.methodMap1.TryGetValue(name, out provider);
        }

        internal bool TryGetDelegate<T>(string name, out Action<T> provider)
        {
            var key = new Key1(name, typeof(T));
            provider = null;
            if (!this.methodMap2.TryGetValue(key, out var action))
            {
                return false;
            }

            provider = (Action<T>) action;
            return true;
        }

        internal bool TryGetDelegate<R>(string name, out Func<R> provider)
        {
            var key = new Key1(name, typeof(R));
            if (!this.methodMap3.TryGetValue(key, out var function))
            {
                provider = default;
                return false;
            }

            provider = (Func<R>) function;
            return true;
        }

        internal bool TryGetDelegate<T, R>(string name, out Func<T, R> provider)
        {
            var key = new Key2(name, typeof(T), typeof(R));
            if (!this.methodMap4.TryGetValue(key, out var function))
            {
                provider = default;
                return false;
            }

            provider = (Func<T, R>) function;
            return true;
        }

        internal bool ExistsMethod1(string name)
        {
            return this.methodMap1.ContainsKey(name);
        }

        internal bool ExistsMethod2<T>(string name)
        {
            var key = new Key1(name, typeof(T));
            return this.methodMap2.ContainsKey(key);
        }

        internal bool ExistsMethod3<R>(string name)
        {
            var key = new Key1(name, typeof(R));
            return this.methodMap3.ContainsKey(key);
        }

        internal bool ExistsMethod4<T, R>(string name)
        {
            var key = new Key2(name, typeof(T), typeof(R));
            return this.methodMap4.ContainsKey(key);
        }
        
        internal void AddMethod1(string name, Action provider)
        {
            if (this.methodMap1.ContainsKey(name))
            {
                throw new Exception($"Method {name}() is already added!");
            }

            this.methodMap1.Add(name, provider);
        }

        internal void AddMethod2<T>(string name, Action<T> provider)
        {
            var key = new Key1(name, typeof(T));
            if (this.methodMap2.ContainsKey(key))
            {
                throw new Exception($"Method {name}({typeof(T).Name}) is already added!");
            }

            this.methodMap2.Add(key, provider);
        }

        internal void AddMethod3<R>(string name, Func<R> provider)
        {
            var key = new Key1(name, typeof(R));
            if (this.methodMap3.ContainsKey(key))
            {
                throw new Exception($"Method {name}({typeof(R).Name}) is already added!");
            }

            this.methodMap3.Add(key, provider);
        }

        internal void AddMethod4<T, R>(string name, Func<T, R> provider)
        {
            var key = new Key2(name, typeof(T), typeof(R));
            if (this.methodMap4.ContainsKey(key))
            {
                throw new Exception($"Method {name}({typeof(T).Name}) : {typeof(R).Name} is already added!");
            }

            this.methodMap4.Add(key, provider);
        }

        internal void RemoveMethod1(string name)
        {
            this.methodMap1.Remove(name);
        }

        internal void RemoveMethod2<T>(string name)
        {
            var key = new Key1(name, typeof(T));
            this.methodMap2.Remove(key);
        }

        internal void RemoveMethod3<T>(string name)
        {
            var key = new Key1(name, typeof(T));
            this.methodMap3.Remove(key);
        }

        internal void RemoveMethod4<T, R>(string name)
        {
            var key = new Key2(name, typeof(T), typeof(R));
            this.methodMap4.Remove(key);
        }
        
        ///Hash Keys
        private readonly struct Key1
        {
            private readonly string name;

            private readonly Type type;

            public Key1(string name, Type type)
            {
                this.name = name;
                this.type = type;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((name != null ? name.GetHashCode() : 0) * 397) ^ (type != null ? type.GetHashCode() : 0);
                }
            }

            public override bool Equals(object obj)
            {
                return obj is Key1 other && Equals(other);
            }

            private bool Equals(Key1 other)
            {
                return name == other.name && type == other.type;
            }
        }

        private readonly struct Key2
        {
            private readonly string name;

            private readonly Type paramType;

            private readonly Type resultType;

            public Key2(string name, Type paramType, Type resultType)
            {
                this.name = name;
                this.paramType = paramType;
                this.resultType = resultType;
            }

            public override bool Equals(object obj)
            {
                return obj is Key2 other && Equals(other);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (name != null ? name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (paramType != null ? paramType.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (resultType != null ? resultType.GetHashCode() : 0);
                    return hashCode;
                }
            }

            private bool Equals(Key2 other)
            {
                return name == other.name && paramType == other.paramType && resultType == other.resultType;
            }
        }
    }
}