using DynamicObjects;
using DynamicObjects.Unity;

public sealed class AttackComponent : MonoDynamicComponent
{
    private MonoDynamicObject target;

    public override void Initialize(MonoDynamicObject target)
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