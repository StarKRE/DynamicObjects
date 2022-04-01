using UnityEngine;

namespace DynamicObjects.Unity
{
    public sealed class MonoObjectProxy : MonoObject
    {
        [SerializeField]
        private MonoObject monoObject;

        protected override IObject TargetObject
        {
            get { return this.monoObject; }
        }
    }
}