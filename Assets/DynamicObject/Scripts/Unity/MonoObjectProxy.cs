using System;
using DynamicObjects;
using UnityEngine;
using PropertyName = DynamicObjects.PropertyName;

namespace Roboland
{
    public sealed class MonoObjectProxy : MonoBehaviour, IObject
    {
        [SerializeField]
        private MonoObject @object;

        public IObject Root
        {
            get { return this.@object.Root; }
        }

        public T Property<T>(PropertyName name)
        {
            return this.@object.Property<T>(name);
        }

        public Func<T> PropertyPtr<T>(PropertyName name)
        {
            return this.@object.PropertyPtr<T>(name);
        }

        public bool TryProperty<T>(PropertyName name, out T property)
        {
            return this.@object.TryProperty<T>(name, out property);
        }

        public bool TryPropertyPtr<T>(PropertyName name, out Func<T> provider)
        {
            return this.@object.TryPropertyPtr(name, out provider);
        }

        public void DefineProperty<T>(PropertyName name, Func<T> provider)
        {
            this.@object.DefineProperty<T>(name, provider);
        }
        
        public bool TryInvoke(MethodName name)
        {
            return this.@object.TryInvoke(name);
        }

        public bool TryInvoke<T>(MethodName name, T data)
        {
            return this.@object.TryInvoke<T>(name, data);
        }

        public bool TryInvoke<R>(MethodName name, out R result)
        {
            return this.@object.TryInvoke(name, out result);
        }

        public bool InvokeMethod<T, R>(MethodName name, T data, out R result)
        {
            return this.@object.InvokeMethod(name, data, out result);
        }

        public bool TryMethodPtr(MethodName name, out Action provider)
        {
            return this.@object.TryMethodPtr(name, out provider);
        }

        public bool TryMethodPtr<T>(MethodName name, out Action<T> provider)
        {
            return this.@object.TryMethodPtr<T>(name, out provider);
        }

        public bool TryMethodPtr<R>(MethodName name, out Func<R> provider)
        {
            return this.@object.TryMethodPtr(name, out provider);
        }

        public bool TryMethodPtr<T, R>(MethodName name, out Func<T, R> provider)
        {
            return this.@object.TryMethodPtr(name, out provider);
        }

        public void DefineMethod(MethodName name, Action provider)
        {
            this.@object.DefineMethod(name, provider);
        }

        public void DefineMethod<T>(MethodName name, Action<T> provider)
        {
            this.@object.DefineMethod<T>(name, provider);
        }

        public void DefineMethod<R>(MethodName name, Func<R> provider)
        {
            this.@object.DefineMethod(name, provider);
        }

        public void DefineMethod<T, R>(MethodName name, Func<T, R> provider)
        {
            this.@object.DefineMethod(name, provider);
        }

        public void DefineEvent(EventName name)
        {
            this.@object.DefineEvent(name);
        }

        public void DefineEvent<T>(EventName name)
        {
            this.@object.DefineEvent<T>(name);
        }

        public void AddListener(EventName name, Action callback)
        {
            this.@object.AddListener(name, callback);
        }

        public void AddListener<T>(EventName name, Action<T> callback)
        {
            this.@object.AddListener<T>(name, callback);
        }

        public void RemoveListener(EventName name, Action callback)
        {
            this.@object.RemoveListener(name, callback);
        }

        public void RemoveListener<T>(EventName name, Action<T> callback)
        {
            this.@object.RemoveListener<T>(name, callback);
        }

        public void Invoke(EventName name)
        {
            this.@object.Invoke(name);
        }

        public void Invoke<T>(EventName name, T data)
        {
            this.@object.Invoke<T>(name, data);
        }
    }
}