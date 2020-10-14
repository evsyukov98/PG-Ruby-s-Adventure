using UnityEngine;

namespace RubyAdventure
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private AudioClip collectedClip;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var controller = other.GetComponent<RubyController>();

            if (controller == null) return;
            if (controller.CurrentHealth >= controller.MaxHealth) return;
                
            controller.ChangeHealth(1);
            Destroy(gameObject);
                    
            controller.PlaySound(collectedClip);
        }
    }
}