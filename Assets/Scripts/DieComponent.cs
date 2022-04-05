using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class DieComponent : MonoBehaviour, MonoDynamicObject.IComponent
{
    private MonoDynamicObject target;
    
    public void Initialize(MonoDynamicObject target)
    {
        target.AddMethod1(CommonKey.Die, this.Die);
        target.DefineEvent(CommonKey.Die);
        this.target = target;
    }

    private void Die()
    {
        this.target.InvokeEvent(CommonKey.Die);
    }
}