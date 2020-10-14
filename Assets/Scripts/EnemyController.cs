using UnityEngine;

namespace RubyAdventure
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private float speed = 3.0f;
        [SerializeField] private bool vertical = false;
        [SerializeField] private float changeTime = 3.0f;
        [SerializeField] private ParticleSystem smokeEffect;
        
        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        
        private float _timer;
        private int _direction = 1;
        private bool _broken = true;
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            
            _timer = changeTime;
        }

        public void Fix()
        {
            _broken = false;
            _rigidbody2D.simulated = false;
            _animator.SetTrigger("Fixed");
            smokeEffect.Stop();
        }
        
        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                _direction = -_direction;
                _timer = changeTime;
            }
            
            if(!_broken)
            {
                return;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            var position = _rigidbody2D.position;

            if (vertical)
            {
                position.y = position.y + Time.deltaTime * speed * _direction;
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", _direction);
            }
            else
            {
                position.x = position.x + Time.deltaTime * speed * _direction;
                _animator.SetFloat("MoveX", _direction);
                _animator.SetFloat("MoveY", 0);
            }
            
            _rigidbody2D.MovePosition(position);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.gameObject.GetComponent<RubyController>();

            if (player != null)
            {
                player.ChangeHealth(-1);
            }
        }
    }
}