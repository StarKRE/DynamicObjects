using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DynamicObjects
{
    [AddComponentMenu("DynamicObjects/Dynamic Object")]
    public sealed class MonoObject : MonoBehaviour, IObject
    {
        private readonly DynamicObject dynamicObject = new DynamicObject();

        [SerializeField]
        private MonoAdapter[] adapters = Array.Empty<MonoAdapter>();

        private void Awake()
        {
            this.SetupObject();
        }

        public IObject Root
        {
            get { return this.dynamicObject; }
        }

        public T Property<T>(PropertyName name)
        {
            return this.dynamicObject.Property<T>(name);
        }

        public Func<T> PropertyPtr<T>(PropertyName name)
        {
            return this.dynamicObject.PropertyPtr<T>(name);
        }

        public bool TryProperty<T>(PropertyName name, out T property)
        {
            return this.dynamicObject.TryProperty(name, out property);
        }

        public bool TryPropertyPtr<T>(PropertyName name, out Func<T> provider)
        {
            return this.dynamicObject.TryPropertyPtr<T>(name, out provider);
        }

        public void DefineProperty<T>(PropertyName name, Func<T> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineProperty<T>(name, provider);
            }

            this.AddPropertyDefinition<T>(name);
#else
            this.dynamicObject.DefineProperty<T>(name, provider);
#endif
        }

        public bool TryInvoke(MethodName name)
        {
            return this.dynamicObject.TryInvoke(name);
        }

        public bool TryInvoke<T>(MethodName name, T data)
        {
            return this.dynamicObject.TryInvoke(name, data);
        }

        public bool TryInvoke<R>(MethodName name, out R result)
        {
            return dynamicObject.TryInvoke(name, out result);
        }

        public bool InvokeMethod<T, R>(MethodName name, T data, out R result)
        {
            return dynamicObject.InvokeMethod(name, data, out result);
        }

        public bool TryMethodPtr(MethodName name, out Action provider)
        {
            return this.dynamicObject.TryGetMethodPtr(name, out provider);
        }

        public bool TryMethodPtr<T>(MethodName name, out Action<T> provider)
        {
            return this.dynamicObject.TryMethodPtr<T>(name, out provider);
        }

        public bool TryMethodPtr<R>(MethodName name, out Func<R> provider)
        {
            return dynamicObject.TryMethodPtr(name, out provider);
        }

        public bool TryMethodPtr<T, R>(MethodName name, out Func<T, R> provider)
        {
            return dynamicObject.TryMethodPtr(name, out provider);
        }

        public void DefineMethod(MethodName name, Action provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineMethod(name, provider);
            }

            this.AddMethodDefinition(name);
#else
            this.dynamicObject.DefineMethod(name, provider);
#endif
        }

        public void DefineMethod<T>(MethodName name, Action<T> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineMethod<T>(name, provider);
            }

            this.AddMethodDefinition<T>(name, provider);
#else
            this.dynamicObject.DefineMethod<T>(name, provider);
#endif
        }

        public void DefineMethod<R>(MethodName name, Func<R> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineMethod<R>(name, provider);
            }

            this.AddMethodDefinition(name, provider);
#else
            this.dynamicObject.DefineMethod<R>(name, provider);
#endif
        }

        public void DefineMethod<T, R>(MethodName name, Func<T, R> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineMethod<T, R>(name, provider);
            }

            this.AddMethodDefinition(name, provider);
#else
            this.dynamicObject.DefineMethod(name, provider);
#endif
        }

        public void AddListener(EventName name, Action callback)
        {
            this.dynamicObject.AddListener(name, callback);
        }

        public void AddListener<T>(EventName name, Action<T> callback)
        {
            this.dynamicObject.AddListener<T>(name, callback);
        }

        public void RemoveListener(EventName name, Action callback)
        {
            this.dynamicObject.RemoveListener(name, callback);
        }

        public void RemoveListener<T>(EventName name, Action<T> callback)
        {
            this.dynamicObject.RemoveListener<T>(name, callback);
        }

        public void Invoke(EventName name)
        {
            this.dynamicObject.Invoke(name);
        }

        public void Invoke<T>(EventName name, T data)
        {
            this.dynamicObject.Invoke(name, data);
        }

        public void DefineEvent(EventName name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineEvent(name);
            }

            this.AddEventDefinition(name);
#else
            this.dynamicObject.DefineEvent(name);
#endif
        }


        public void DefineEvent<T>(EventName name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineEvent<T>(name);
            }

            this.AddEventDefinition<T>(name);
#else
            this.dynamicObject.DefineEvent<T>(name);
#endif
        }

        private void SetupObject()
        {
            for (int i = 0, count = this.adapters.Length; i < count; i++)
            {
                var adapter = this.adapters[i];
                if (adapter != null)
                {
                    this.SetupAdapter(adapter);
                }
            }
        }

        private void SetupAdapter(MonoAdapter adapter)
        {
            if (!adapter.gameObject.activeSelf)
            {
                return;
            }

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                adapter.SetupObject(this);
            }
            else
            {
                try
                {
                    adapter.SetupObject(this);
                }
                catch (Exception)
                {
                    throw new AdapterExeption(adapter);
                }
            }
#else
            adapter.SetupObject(this);
#endif
        }

#if UNITY_EDITOR
        private readonly HashSet<PropertyDefinition> propertyDefinitions = new HashSet<PropertyDefinition>();

        private readonly HashSet<MethodDefinition> methodDefinitions = new HashSet<MethodDefinition>();

        private readonly HashSet<EventDefinition> eventDefinitions = new HashSet<EventDefinition>();

        public void UpdateObjectInEditor()
        {
            this.propertyDefinitions.Clear();
            this.methodDefinitions.Clear();
            this.eventDefinitions.Clear();
            this.SetupObject();
        }

        private void AddPropertyDefinition<T>(PropertyName name)
        {
            var definition = new PropertyDefinition(name, typeof(T));
            this.propertyDefinitions.Add(definition);
        }

        private void AddMethodDefinition(MethodName name)
        {
            var definition = MethodDefinition.Create(name);
            this.methodDefinitions.Add(definition);
        }

        private void AddMethodDefinition<T>(MethodName name, Action<T> action)
        {
            var definition = MethodDefinition.Create(name, action);
            this.methodDefinitions.Add(definition);
        }

        private void AddMethodDefinition<R>(MethodName name, Func<R> func)
        {
            var definition = MethodDefinition.Create(name, func);
            this.methodDefinitions.Add(definition);
        }

        private void AddMethodDefinition<T, R>(MethodName name, Func<T, R> func)
        {
            var definition = MethodDefinition.Create(name, func);
            this.methodDefinitions.Add(definition);
        }

        private void AddEventDefinition(EventName name)
        {
            var definition = new EventDefinition(name, null);
            this.eventDefinitions.Add(definition);
        }

        private void AddEventDefinition<T>(EventName name)
        {
            var definition = new EventDefinition(name, typeof(T));
            this.eventDefinitions.Add(definition);
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
#endif
    }
}