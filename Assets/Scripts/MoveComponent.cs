using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class MoveComponent : MonoDynamicComponent
{
    [SerializeField]
    private Transform root;
    
    public override void Initialize(MonoDynamicObject target)
    {
        target.AddMethod2<Vector3>(CommonKey.Move, this.Move);
    }

    private void Move(Vector3 direction)
    {
        this.root.position += direction * Time.deltaTime;
    }
}