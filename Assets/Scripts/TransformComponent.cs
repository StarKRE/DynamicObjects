using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class TransformComponent : MonoBehaviour, MonoDynamicObject.IComponent
{
    [SerializeField]
    private Transform root;

    public void Initialize(MonoDynamicObject target)
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