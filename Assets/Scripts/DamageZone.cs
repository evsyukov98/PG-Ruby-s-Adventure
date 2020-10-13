using UnityEngine;

namespace RubyAdventure
{
    public class DamageZone : MonoBehaviour 
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            RubyController controller = other.GetComponent<RubyController >();

            if (controller != null)
            {
                controller.ChangeHealth(-1);
            }
        } 
    }
}
