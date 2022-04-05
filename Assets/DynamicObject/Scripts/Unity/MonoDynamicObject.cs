using System;
using System.Collections.Generic;
using System.Linq;
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
        private MonoBehaviour[] components = Array.Empty<MonoBehaviour>();

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
                var monoBehaviour = this.components[i];
                if (monoBehaviour != null && monoBehaviour.gameObject.activeSelf && monoBehaviour is IComponent component)
                {
                    InitializeComponent(component);
                }
            }
        }

        private void InitializeComponent(IComponent component)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                component.Initialize(this);
                return;
            }

            try
            {
                component.Initialize(this);
            }
            catch (Exception)
            {
                throw new ComponentException(component);
            }
#else
            component.Initialize(this);
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
        
        ///Interface
        public interface IComponent
        {
            void Initialize(MonoDynamicObject target);
        }

        #region Editor

#if UNITY_EDITOR

        public Metadata Info { get; } = new Metadata();

        public void UpdateInEditor()
        {
            this.Info.Clear();
            this.Initialize();
        }
        
        ///Exception
        public sealed class ComponentException : Exception
        {
            public IComponent Component { get; }

            public ComponentException(IComponent component)
            {
                this.Component = component;
            }
        }

        ///Metadata
        public sealed class Metadata
        {
            private readonly HashSet<PropertyDefinition> propertyDefinitions;

            private readonly HashSet<MethodDefinition> methodDefinitions;

            private readonly HashSet<EventDefinition> eventDefinitions;

            public Metadata()
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
                    this.text = $"{this.name} : {Utils.PrettyName(type)}";
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
                        return ((name != null ? name.GetHashCode() : 0) * 397) ^
                               (text != null ? text.GetHashCode() : 0);
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
                    var text = $"{name}({Utils.PrettyName(type)})";
                    return new MethodDefinition(name, text);
                }

                public static MethodDefinition Create3<R>(string name)
                {
                    var type = typeof(R);
                    var text = $"{name}() : {Utils.PrettyName(type)}";
                    return new MethodDefinition(name, text);
                }

                public static MethodDefinition Create4<T, R>(string name)
                {
                    var paramType = typeof(T);
                    var resultType = typeof(R);
                    var text = $"{name}({Utils.PrettyName(paramType)}) : {Utils.PrettyName(resultType)}";
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
                        this.text = $"{this.name}({Utils.PrettyName(this.type)})";
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

            ///Utils 
            private static class Utils
            {
                public static string PrettyName(System.Type type)
                {
                    if (!type.IsGenericType)
                    {
                        return BaseName(type);
                    }

                    var genericArguments = type.GetGenericArguments()
                        .Select(PrettyName)
                        .Aggregate((x1, x2) => $"{x1}, {x2}");

                    var name = BaseName(type);
                    return $"{name.Substring(0, name.IndexOf("`", StringComparison.Ordinal))}" +
                           $"<{genericArguments}>";
                }

                private static string BaseName(System.Type type)
                {
                    var typeName = type.Name;
                    return typeName switch
                    {
                        "Boolean" => "bool",
                        "Int32" => "int",
                        "String" => "string",
                        "Single" => "float",
                        _ => typeName
                    };
                }
            }
        }
#endif

        #endregion
    }
}