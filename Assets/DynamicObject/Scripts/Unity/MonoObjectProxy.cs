using UnityEngine;

namespace DynamicObjects.Unity
{
    [AddComponentMenu("DynamicObjects/Proxy")]
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