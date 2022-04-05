using DynamicObjects;
using DynamicObjects.Unity;
using UnityEngine;

public sealed class MoveComponent : MonoBehaviour, MonoDynamicObject.IComponent
{
    [SerializeField]
    private Transform root;
    
    public void Initialize(MonoDynamicObject target)
    {
        target.AddMethod2<Vector3>(CommonKey.Move, this.Move);
    }

    private void Move(Vector3 direction)
    {
        this.root.position += direction * Time.deltaTime;
    }
}