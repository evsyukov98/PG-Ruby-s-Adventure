using UnityEngine;

namespace RubyAdventure 
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class RubyController : MonoBehaviour
    {
        private readonly string[] _interactionLayers = { "NPC" };
        private static readonly int AnimatorLaunch = Animator.StringToHash("Launch");
        private static readonly int AnimatorLookX = Animator.StringToHash("Look X");
        private static readonly int AnimatorSpeed = Animator.StringToHash("Speed");
        private static readonly int AnimatorLookY = Animator.StringToHash("Look Y");
        
        [SerializeField] private int maxHealth = 5;
        [SerializeField] private float speed = 5f;
        [SerializeField] private float timeInvincible = 1.0f;

        private bool _isInvincible;
        private float _invincibleTimer;

        private Animator _animator;
        private Rigidbody2D _rigidbody2D;
        private AudioSource _audioSource;

        private Vector2 _lookDirection = new Vector2(1,0);

        public int CurrentHealth { get; private set; }

        public  int MaxHealth => maxHealth;
        
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

        private void Update()
        {
            InteractionNPC(InputManager.IsActionPressed);
            Launch(InputManager.IsFirePressed);
        }

        private void FixedUpdate()
        {
            AnimateControl(InputManager.Horizontal, InputManager.Vertical);
            ActivateInvincible();
            Move(speed, InputManager.Horizontal, InputManager.Vertical);
        }


        private void InteractionNPC(bool activate)
        {
            if (!activate) return;
            
            var hit = Physics2D.Raycast(
                _rigidbody2D.position + Vector2.up * 0.2f,
                _lookDirection, 
                1.5f, 
                LayerMask.GetMask(_interactionLayers));

            if (hit.collider == null) return;
            
            if (hit.collider.TryGetComponent<NonPlayerCharacter>(out var character))
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
            
            _animator.SetFloat(AnimatorLookX, _lookDirection.x);
            _animator.SetFloat(AnimatorLookY, _lookDirection.y);
            // magnitude - длина вектора (скорость).
            _animator.SetFloat(AnimatorSpeed, move.magnitude);
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
            if(!activate) return;

            var projectileObject = PoolManager.instance.GetPoolObject(PoolType.Cog);
            projectileObject.transform.position = _rigidbody2D.position + Vector2.up * 0.5f;
            
            if (projectileObject.TryGetComponent<Projectile>(out var projectile))
            {
                projectile.Launch(_lookDirection, 300);
            }
            _animator.SetTrigger(AnimatorLaunch);
        }
    }
}