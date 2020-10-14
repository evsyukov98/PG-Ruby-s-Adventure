using UnityEngine;

namespace RubyAdventure
{
    public class DamageZone : MonoBehaviour 
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            if (other.TryGetComponent<RubyController>(out var controller))
            {
                controller.ChangeHealth(-1);
            }
        } 
    }
}
