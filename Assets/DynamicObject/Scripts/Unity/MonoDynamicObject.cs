using System;
using UnityEditor;
using UnityEngine;

namespace DynamicObjects.Unity
{
    [AddComponentMenu("DynamicObjects/Dynamic Object")]
    public sealed class MonoDynamicObject : MonoObject
    {
        protected override IObject TargetObject
        {
            get { return this.dynamicObject; }
        }

        private readonly DynamicObject dynamicObject;

        [SerializeField]
        private bool initializeOnAwake = true;

        [Space]
        [SerializeField]
        private MonoDynamicComponent[] components = Array.Empty<MonoDynamicComponent>();

        #region Initialization

        public MonoDynamicObject()
        {
            this.dynamicObject = new DynamicObject();
        }

        private void Awake()
        {
            if (this.initializeOnAwake)
            {
                this.Initialize();
            }
        }

        public void Initialize()
        {
            this.InitializeComponents();
        }

        private void InitializeComponents()
        {
            for (int i = 0, count = this.components.Length; i < count; i++)
            {
                var component = this.components[i];
                if (component != null)
                {
                    InitializeComponent(component);
                }
            }
        }

        private void InitializeComponent(MonoDynamicComponent component)
        {
            if (!component.gameObject.activeSelf)
            {
                return;
            }

#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                component.Initialize(this);
            }
            else
            {
                try
                {
                    component.Initialize(this);
                }
                catch (Exception)
                {
                    throw new ComponentExeption(component);
                }
            }
#else
            component.Initialize(target);
#endif
        }

        #endregion

        #region Properties

        public void AddProperty<T>(string name, Func<T> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.AddProperty<T>(name, provider);
            }

            this.Info.AddProperty<T>(name);
#else
            this.dynamicObject.AddProperty<T>(name, provider);
#endif
        }

        public void RemoveProperty<T>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.RemoveProperty<T>(name);
            }

            this.Info.RemoveProperty<T>(name);
#else
            this.dynamicObject.RemoveProperty<T>(name);
#endif
        }

        #endregion

        #region Methods

        public void AddMethod1(string name, Action provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.AddMethod1(name, provider);
            }

            this.Info.AddMethod1(name);
#else
            this.dynamicObject.AddMethod1(name, provider);
#endif
        }

        public void RemoveMethod1(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.RemoveMethod1(name);
            }

            this.Info.RemoveMethod1(name);
#else
            this.dynamicObject.RemoveMethod1(name);
#endif
        }

        public void AddMethod2<T>(string name, Action<T> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.AddMethod2(name, provider);
            }

            this.Info.AddMethod2<T>(name);
#else
            this.dynamicObject.AddMethod2(name, provider);
#endif
        }

        public void RemoveMethod2<T>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.RemoveMethod2<T>(name);
            }

            this.Info.RemoveMethod2<T>(name);
#else
            this.dynamicObject.RemoveMethod2<T>(name);
#endif

            this.dynamicObject.RemoveMethod2<T>(name);
        }

        public void AddMethod3<R>(string name, Func<R> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.AddMethod3(name, provider);
            }

            this.Info.AddMethod3<R>(name);
#else
            this.dynamicObject.AddMethod3(name, provider);
#endif
        }

        public void RemoveMethod3<R>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.RemoveMethod3<R>(name);
            }

            this.Info.RemoveMethod3<R>(name);
#else
            this.dynamicObject.RemoveMethod3<R>(name);
#endif
        }

        public void AddMethod4<T, R>(string name, Func<T, R> provider)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.AddMethod4(name, provider);
            }

            this.Info.AddMethod4<T, R>(name);
#else
            this.dynamicObject.AddMethod4(name, provider);
#endif
        }

        public void RemoveMethod4<T, R>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.RemoveMethod4<T, R>(name);
            }

            this.Info.RemoveMethod4<T, R>(name);
#else
            this.dynamicObject.RemoveMethod4<T, R>(name);
#endif
        }

        #endregion

        #region Events

        public void InvokeEvent(string name)
        {
            this.dynamicObject.InvokeEvent(name);
        }

        public void InvokeEvent<T>(string name, T data)
        {
            this.dynamicObject.InvokeEvent(name, data);
        }

        public void DefineEvent(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineEvent(name);
            }

            this.Info.AddEvent(name);
#else
            this.dynamicObject.DefineEvent(name);
#endif
        }

        public void DisposeEvent(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DisposeEvent(name);
            }

            this.Info.RemoveEvent(name);
#else
            this.Info.RemoveEvent(name);
#endif
        }

        public void DefineEvent<T>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DefineEvent<T>(name);
            }

            this.Info.AddEvent<T>(name);
#else
            this.dynamicObject.DefineEvent<T>(name);
#endif
        }

        public void DisposeEvent<T>(string name)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                this.dynamicObject.DisposeEvent<T>(name);
            }
            
            this.Info.RemoveEvent<T>(name);
#else
            this.dynamicObject.DisposeEvent<T>(name);
#endif
        }

        #endregion

        #region Editor

#if UNITY_EDITOR

        public ObjectInfo Info { get; } = new ObjectInfo();

        public void UpdateInEditor()
        {
            this.Info.Clear();
            this.Initialize();
        }
#endif

        #endregion
    }
}