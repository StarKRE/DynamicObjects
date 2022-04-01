using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class TransformComponent : MonoDynamicComponent
{
    [SerializeField]
    private Transform root;
    
    public override void Initialize(MonoDynamicObject target)
    {
        target.AddProperty<Vector3>(CommonKey.Position, this.GetPosiition);
        target.AddProperty<Vector3>(CommonKey.Rotation, this.GetRotation);
    }

    private Vector3 GetPosiition()
    {
        return this.root.position;
    }

    private Vector3 GetRotation()
    {
        return this.root.eulerAngles;
    }
}