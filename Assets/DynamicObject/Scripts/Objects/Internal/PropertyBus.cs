using System;
using System.Collections.Generic;

namespace DynamicObjects
{
    internal sealed class PropertyBus
    {
        private readonly Dictionary<string, Delegates> delegateTable;

        internal PropertyBus()
        {
            this.delegateTable = new Dictionary<string, Delegates>();
        }

        internal bool PropertyExists<T>(string name)
        {
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                return false;
            }

            return delegates.Exists<T>();
        }
        
        internal T GetProperty<T>(string name)
        {
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                ThrowNotFoundException<T>(name);
            }

            if (!delegates.TryGet<T>(out var provider))
            {
                ThrowNotFoundException<T>(name);
            }

            return provider.Invoke();
        }

        internal bool TryGetProperty<T>(string name, out T property)
        {
            property = default;
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                return false;
            }

            if (!delegates.TryGet<T>(out var provider))
            {
                return false;
            }

            property = provider.Invoke();
            return true;
        }

        internal bool TryGetDelegate<T>(string name, out Func<T> provider)
        {
            provider = null;
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                return false;
            }

            return delegates.TryGet<T>(out provider);
        }

        internal Func<T> GetDelegate<T>(string name)
        {
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                ThrowNotFoundException<T>(name);
            }

            if (!delegates.TryGet<T>(out var provider))
            {
                ThrowNotFoundException<T>(name);
            }

            return provider;
        }

        internal void AddProperty<T>(string name, Func<T> provider)
        {
            if (!this.delegateTable.TryGetValue(name, out var delegates))
            {
                delegates = new Delegates();
                this.delegateTable.Add(name, delegates);
            }

            if (!delegates.Add(provider))
            {
                ThrowAlreadyAddedException<T>(name);
            }
        }

        internal void RemoveProperty<T>(string name)
        {
            if (this.delegateTable.TryGetValue(name, out var delegates))
            {
                delegates.Remove<T>();
            }
        }

        private static void ThrowAlreadyAddedException<T>(string name)
        {
            throw new Exception($"Property {name} of type {typeof(T).Name} is already added!");
        }

        private static void ThrowNotFoundException<T>(string name)
        {
            var typeName = typeof(T).Name;
            throw new Exception($"Property {name} of type {typeName} is not found!");
        }
        
        /// Delegate Dictionary
        private sealed class Delegates
        {
            private readonly Dictionary<Type, object> delegates;

            internal Delegates()
            {
                this.delegates = new Dictionary<Type, object>();
            }

            internal bool Exists<T>()
            {
                var type = typeof(T);
                return this.delegates.ContainsKey(type);
            }

            internal bool Add<T>(Func<T> function)
            {
                var type = typeof(T);
                if (this.delegates.ContainsKey(type))
                {
                    return false;
                }

                this.delegates.Add(type, function);
                return true;
            }

            internal bool Remove<T>()
            {
                var type = typeof(T);
                return this.delegates.Remove(type);
            }

            internal bool TryGet<T>(out Func<T> function)
            {
                var requiredType = typeof(T);
                if (this.delegates.TryGetValue(requiredType, out var value))
                {
                    function = (Func<T>) value;
                    return true;
                }

                function = default;
                return false;
            }
        }
    }
}