using System;
using DynamicObjects.Unity;
using UnityEngine;
using static DynamicObjects.CommonKey;

namespace DefaultNamespace
{
    public sealed class Test : MonoBehaviour
    {
        [SerializeField]
        private MonoObject player;

        private Action<Vector3> movePtr;

        private void Start()
        {
            var position = this.player.GetProperty<Vector3>(Position);
            var rotation = this.player.GetProperty<Vector3>(Rotation);

            Debug.Log($"Player position {position}");
            Debug.Log($"Player rotation {rotation}");

            this.player.TryGetMethodPtr(Move, out this.movePtr);
            
            this.player.AddListener(Attack, this.OnAttack);
        }

        private void OnDestroy()
        {
            this.player.RemoveListener(Attack, this.OnAttack);
        }
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.movePtr.Invoke(new Vector3(0, 0, 1));
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.movePtr.Invoke(new Vector3(0, 0, -1));
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.movePtr.Invoke(new Vector3(-1, 0, 0));
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.movePtr.Invoke(new Vector3(1, 0, 0));
            }

            if (Input.GetKey(KeyCode.A))
            {
                this.player.CallMethod(Attack);
            }
        }
        
        private void OnAttack()
        {
            Debug.Log("ATTACK CALLBACK");
        }
    }
}