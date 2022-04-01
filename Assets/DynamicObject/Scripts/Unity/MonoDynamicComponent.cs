using UnityEngine;

namespace DynamicObjects.Unity
{
    public abstract class MonoDynamicComponent : MonoBehaviour
    {
        public abstract void Initialize(MonoDynamicObject target);
    }
}