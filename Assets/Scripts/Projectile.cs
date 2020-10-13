using RubyAdventure;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        _rigidbody2D.AddForce(direction * force);
    }
    
    private void Update()
    {
        if(transform.position.magnitude > 200.0f)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController e = other.collider.GetComponent<EnemyController>();

        if (e != null)
        {
            e.Fix();
        }
        
        Destroy(this.gameObject);
    }
}
