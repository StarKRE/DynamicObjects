#if UNITY_EDITOR
using System;

namespace DynamicObjects
{
    public sealed class AdapterExeption : Exception
    {
        public MonoAdapter Adapter { get; }

        public AdapterExeption(MonoAdapter adapter)
        {
            this.Adapter = adapter;
        }
    }
}
#endif