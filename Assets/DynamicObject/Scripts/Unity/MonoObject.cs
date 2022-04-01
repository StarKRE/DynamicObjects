using System;
using UnityEngine;

namespace DynamicObjects.Unity
{
    public abstract class MonoObject : MonoBehaviour, IObject
    {
        public IObject Root
        {
            get { return this.TargetObject; }
        }

        protected abstract IObject TargetObject { get; }

        public T GetProperty<T>(string name)
        {
            return this.TargetObject.GetProperty<T>(name);
        }

        public bool TryGetProperty<T>(string name, out T property)
        {
            return this.TargetObject.TryGetProperty(name, out property);
        }

        public bool TryGetPropertyPtr<T>(string name, out Func<T> provider)
        {
            return this.TargetObject.TryGetPropertyPtr(name, out provider);
        }

        public void CallMethod(string name)
        {
            this.TargetObject.CallMethod(name);
        }

        public void CallMethod<T>(string name, T data)
        {
            this.TargetObject.CallMethod<T>(name, data);
        }

        public R CallMethod<R>(string name)
        {
            return this.TargetObject.CallMethod<R>(name);
        }

        public R CallMethod<T, R>(string name, T data)
        {
            return this.TargetObject.CallMethod<T, R>(name, data);
        }

        public bool TryGetMethodPtr(string name, out Action provider)
        {
            return this.TargetObject.TryGetMethodPtr(name, out provider);
        }

        public bool TryGetMethodPtr<T>(string name, out Action<T> provider)
        {
            return this.TargetObject.TryGetMethodPtr(name, out provider);
        }

        public bool TryGetMethodPtr<R>(string name, out Func<R> provider)
        {
            return this.TargetObject.TryGetMethodPtr(name, out provider);
        }

        public bool TryGetMethodPtr<T, R>(string name, out Func<T, R> provider)
        {
            return this.TargetObject.TryGetMethodPtr(name, out provider);
        }

        public void AddListener(string name, Action callback)
        {
            this.TargetObject.AddListener(name, callback);
        }

        public void AddListener<T>(string name, Action<T> callback)
        {
            this.TargetObject.AddListener(name, callback);
        }

        public void RemoveListener(string name, Action callback)
        {
            this.TargetObject.RemoveListener(name, callback);
        }

        public void RemoveListener<T>(string name, Action<T> callback)
        {
            this.TargetObject.RemoveListener(name, callback);
        }
    }
}