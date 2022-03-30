#if UNITY_EDITOR
using System;

namespace DynamicObjects
{
    public sealed class AdapterExeption : Exception
    {
        public MonoDynamicAdapter Adapter { get; }

        public AdapterExeption(MonoDynamicAdapter adapter)
        {
            this.Adapter = adapter;
        }
    }
}
#endif