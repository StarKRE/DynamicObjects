using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class AttackComponent : MonoBehaviour, MonoDynamicObject.IComponent
{
    private MonoDynamicObject target;

    public void Initialize(MonoDynamicObject target)
    {
        target.AddMethod1(CommonKey.Attack, this.Attack);
        target.DefineEvent(CommonKey.Attack);
        this.target = target;
    }

    private void Attack()
    {
        this.target.InvokeEvent(CommonKey.Attack);
    }
}