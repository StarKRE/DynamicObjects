using UnityEngine;

namespace DynamicObjects
{
    public abstract class MonoDynamicAdapter : MonoBehaviour
    {
        public abstract void SetupObject(IObject target);
    }
}