using UnityEngine;

namespace RubyAdventure
{
    public class HealthCollectible : MonoBehaviour
    {
        [SerializeField] private AudioClip collectedClip = default;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent<RubyController>(out var controller)) return;
            if (controller.CurrentHealth >= controller.MaxHealth) return;
                
            controller.ChangeHealth(1);
            controller.PlaySound(collectedClip);
            
            Destroy(gameObject);
        }
    }
}