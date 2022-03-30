using UnityEngine;

namespace DynamicObjects
{
    public abstract class MonoAdapter : MonoBehaviour
    {
        public abstract void SetupObject(IObject target);
    }
}