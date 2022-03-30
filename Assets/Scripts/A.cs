using UnityEngine;

namespace DynamicObjects
{
    public class A
    {
        private const string Position = "Position";
        
        private const string Attack = "Attack";

        private void B(IObject player)
        {
            var position2 = player.GetProperty<Vector3>(Position);
            
            player.CallMethod(Attack);
            
            player.AddListener(Attack, this.OnAttack);
        }

        private void OnAttack()
        {
            
        }
    }
}