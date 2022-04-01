#if UNITY_EDITOR
using System;

namespace DynamicObjects.Unity
{
    public sealed class ComponentExeption : Exception
    {
        public MonoDynamicComponent DynamicComponent { get; }

        public ComponentExeption(MonoDynamicComponent component)
        {
            this.DynamicComponent = component;
        }
    }
}
#endif