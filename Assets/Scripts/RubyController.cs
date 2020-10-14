using UnityEngine;

namespace RubyAdventure 
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]

    public class RubyController : MonoBehaviour
    {
        public  int CurrentHealth { get; private set; }

        public  int MaxHealth => maxHealth;

        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float timeInvincible = 1.0f;

        private bool _isInvincible;
        private float _invincibleTimer;

        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private AudioSource _audioSource;

        private Vector2 _lookDirection = new Vector2(1,0);

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _audioSource = GetComponent<AudioSource>();
            
            CurrentHealth = maxHealth;
        }
        
        public void ChangeHealth(int amount)
        {
            if (amount < 0)
            {
                if (_isInvincible) return;

                _isInvincible = true;
                _invincibleTimer = timeInvincible;
            }
            
            CurrentHealth = Mathf.Clamp(CurrentHealth + amount, 0, maxHealth);
            
            UIHealthBar.Instance.SetValue(CurrentHealth / (float)maxHealth);
        }
        
        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        private void FixedUpdate()
        {
            AnimateControl(InputManager.Horizontal,InputManager.Vertical);
            ActivateInvincible();
            Move(speed, InputManager.Horizontal, InputManager.Vertical);
        }

        private void Update()
        {
            InteractionNPC(InputManager.X);
            Launch(InputManager.C);
        }


        private void InteractionNPC(bool activate)
        {
            if (!activate) return;
            
            var hit = Physics2D.Raycast(
                _rigidbody2D.position + Vector2.up * 0.2f,
                _lookDirection, 
                1.5f, 
                LayerMask.GetMask("NPC"));

            if (hit.collider == null) return;
                
            var character = hit.collider.GetComponent<NonPlayerCharacter>();
                    
            if (character != null)
            {
                character.DisplayDialog();
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

        private void Launch(bool activate)
        {
            if (!activate) return;
            
            var projectileObject = Instantiate(projectilePrefab,
                _rigidbody2D.position + Vector2.up * 0.5f, Quaternion.identity);

            var projectile = projectileObject.GetComponent<Projectile>();
            projectile.Launch(_lookDirection, 300);

            _animator.SetTrigger("Launch");
        }
    }
}