using UnityEngine;

namespace RubyAdventure
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour, IPoolable
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (transform.position.sqrMagnitude > 40000.0f)
            {
                OnDespawn();
            }
        }

        public void Launch(Vector2 direction, float force)
        {
            _rigidbody2D.AddForce(direction * force);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent<EnemyController>(out var enemyController))
            {
                enemyController.Fix();
            } 
            
            OnDespawn();
        }

        public void OnSpawn()
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            gameObject.SetActive(true);
        }

        public void OnDespawn()
        {
            gameObject.SetActive(false);
        }
    }
}
