using UnityEngine;

namespace RubyAdventure 
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RubyController : MonoBehaviour
    {
        public  int CurrentHealth => _currentHealth;
        public  int MaxHealth => maxHealth;

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private float speed = 5f;

        private int _currentHealth;
        
        private readonly float _timeInvincible = 1.0f;
        private bool _isInvincible;
        private float _invincibleTimer;

        private Animator _animator;
        
        private Rigidbody2D _rigidbody2D;
        
        private Vector2 _lookDirection = new Vector2(1,0);

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _currentHealth = maxHealth;
        }
        
        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                if (_isInvincible) return;

                _isInvincible = true;
                _invincibleTimer = _timeInvincible;
            }
            
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, maxHealth);
            
            Debug.Log($"{_currentHealth.ToString()}/{maxHealth.ToString()}");
        }

        private void Update()
        {
            var hor = Input.GetAxis("Horizontal");
            var ver = Input.GetAxis("Vertical");
            
            AnimateControl(hor,ver);
            ActivateInvincible();
            Move(speed, hor, ver);
            
            if(Input.GetKeyDown(KeyCode.C))
            {
                Launch();
            }
        }

        private void AnimateControl(float horizontal, float vertical)
        {
            var move = new Vector2(horizontal, vertical);
            
            // Mathf.Approximately(x,y) -
            // приблизительное сравнение для чисел с плавающей точкой.
            if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
            {
                _lookDirection.Set(move.x, move.y);
                
                // normalize - округление до 1 с сохранением знака.
                _lookDirection.Normalize();
            }
            
            _animator.SetFloat("Look X", _lookDirection.x);
            _animator.SetFloat("Look Y", _lookDirection.y);
            // magnitude - длина вектора (скорость).
            _animator.SetFloat("Speed", move.magnitude);
        }

        private void ActivateInvincible()
        {
            if (!_isInvincible) return;
            
            _invincibleTimer -= Time.deltaTime;
            if (_invincibleTimer < 0) _isInvincible = false;
        }
        
        private void Move(float moveSpeed, float horizontal, float vertical)
        {
            var position = transform.position;
            position.x += moveSpeed * horizontal * Time.deltaTime;
            position.y += moveSpeed * vertical * Time.deltaTime;

            _rigidbody2D.MovePosition(position);
        }

        private void Launch()
        {
            GameObject projectileObject = Instantiate(projectilePrefab, 
                _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

            Projectile projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(_lookDirection, 300);

            _animator.SetTrigger("Launch");
        }
    }
}
