using System;

namespace DynamicObjects
{
    public sealed class DynamicObject : IObject
    {
        public IObject Root
        {
            get { return this; }
        }

        private PropertyBus _properties;

        private MethodBus _methods;

        private EventBus _events;
        
        private PropertyBus properties
        {
            get
            {
                if (this._properties == null)
                {
                    this._properties = new PropertyBus();
                }

                return this._properties;
            }
        }

        private MethodBus methods
        {
            get
            {
                if (this._methods == null)
                {
                    this._methods = new MethodBus();
                }

                return this._methods;
            }
        }

        private EventBus events
        {
            get
            {
                if (this._events == null)
                {
                    this._events = new EventBus();
                }

                return this._events;
            }
        }

        ///Properties 
        public T GetProperty<T>(string name)
        {
            return this.properties.GetProperty<T>(name);
        }

        public Func<T> GetPropertyPtr<T>(string name)
        {
            return this.properties.GetDelegate<T>(name);
        }

        public bool TryGetProperty<T>(string name, out T property)
        {
            return this.properties.TryGetProperty(name, out property);
        }

        public bool TryGetPropertyPtr<T>(string name, out Func<T> provider)
        {
            return this.properties.TryGetDelegate(name, out provider);
        }

        public void AddProperty<T>(string name, Func<T> provider)
        {
            this.properties.AddProperty(name, provider);
        }

        public void RemoveProperty<T>(string name)
        {
            this.properties.RemoveProperty<T>(name);
        }

        ///Methods 
        public void CallMethod(string name)
        {
            this.methods.CallMethod(name);
        }

        public void CallMethod<T>(string name, T data)
        {
            this.methods.CallMethod<T>(name, data);
        }

        public R CallMethod<R>(string name)
        {
            return this.methods.CallMethod<R>(name);
        }

        public R CallMethod<T, R>(string name, T data)
        {
            return this.methods.CallMethod<T, R>(name, data);
        }

        public bool TryGetMethodPtr(string name, out Action provider)
        {
            return this.methods.TryGetDelegate(name, out provider);
        }

        public bool TryGetMethodPtr<T>(string name, out Action<T> provider)
        {
            return this.methods.TryGetDelegate<T>(name, out provider);
        }

        public bool TryGetMethodPtr<R>(string name, out Func<R> provider)
        {
            return this.methods.TryGetDelegate<R>(name, out provider);
        }

        public bool TryGetMethodPtr<T, R>(string name, out Func<T, R> provider)
        {
            return this.methods.TryGetDelegate<T, R>(name, out provider);
        }

        public void AddMethod1(string name, Action provider)
        {
            this.methods.AddMethod1(name, provider);
        }

        public void AddMethod2<T>(string name, Action<T> provider)
        {
            this.methods.AddMethod2(name, provider);
        }

        public void AddMethod3<R>(string name, Func<R> provider)
        {
            this.methods.AddMethod3(name, provider);
        }

        public void AddMethod4<T, R>(string name, Func<T, R> provider)
        {
            this.methods.AddMethod4(name, provider);
        }

        public void RemoveMethod1(string name)
        {
            this.methods.RemoveMethod1(name);
        }

        public void RemoveMethod2<T>(string name)
        {
            this.methods.RemoveMethod2<T>(name);
        }

        public void RemoveMethod3<R>(string name)
        {
            this.methods.RemoveMethod3<R>(name);
        }

        public void RemoveMethod4<T, R>(string name)
        {
            this.methods.RemoveMethod4<T, R>(name);
        }

        ///Events:
        public void AddListener(string name, Action callback)
        {
            this.events.AddListener(name, callback);
        }

        public void AddListener<T>(string name, Action<T> callback)
        {
            this.events.AddListener(name, callback);
        }

        public void RemoveListener(string name, Action callback)
        {
            this.events.RemoveListener(name, callback);
        }

        public void RemoveListener<T>(string name, Action<T> callback)
        {
            this.events.RemoveListener<T>(name, callback);
        }

        public void DefineEvent(string name)
        {
            this.events.DefineEvent(name);
        }

        public void DefineEvent<T>(string name)
        {
            this.events.DefineEvent<T>(name);
        }

        public void DisposeEvent(string name)
        {
            this.events.DisposeEvent(name);
        }

        public void DisposeEvent<T>(string name)
        {
            this.events.DisposeEvent<T>(name);
        }

        public void InvokeEvent(string name)
        {
            this.events.InvokeEvent(name);
        }

        public void InvokeEvent<T>(string name, T data)
        {
            this.events.InvokeEvent(name, data);
        }
    }
}