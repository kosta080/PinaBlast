using Infra;
using Kosta.Controls;
using UnityEngine;
using Kosta.Infra;
using Kosta.Items;

namespace Kosta.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _runSpeed = 5f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] public Collider2D _feetCollider;
        [SerializeField] private float _maxVerticalSpeed = 10f;
        private PickingManager _pickingManager;

        private bool _movementAllowed = true;
        private bool _isGrounded = true;

        enum CharacterAnimations
        {
            Idle, Run, Jump, Land
        }
        
        private CharacterAnimations _currentAnimation = CharacterAnimations.Idle;
        private PlayerController _playerController;
        private EventManager _eventManager;
        
        private void Start()
        {
            _playerController = ServiceLocator.Resolve<PlayerController>();
            _pickingManager = ServiceLocator.Resolve<PickingManager>();
            _eventManager = ServiceLocator.Resolve<EventManager>();

            _eventManager.OnFinalSpawnFinished += () => { _movementAllowed = false;};
            _eventManager.OnRestartRound += () => { _movementAllowed = true;};
        }
        
        void Update()
        {
            if (_playerController.IsKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
            
            if (_playerController.IsKeyDown(KeyCode.RightArrow))
            {
                RunRight();
            }
            else if (_playerController.IsKeyDown(KeyCode.LeftArrow))
            {
                RunLeft();
            }
            else
            {
                TriggerAnimation(CharacterAnimations.Idle);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GlobalValues.TagWalkable) 
                && _feetCollider.IsTouching(other.collider))
            {
                _isGrounded = true;
                TriggerAnimation(CharacterAnimations.Land);
            }

            if (other.gameObject.CompareTag(GlobalValues.TagPickable))
            {
                _pickingManager.HandlePicking(other.collider);
                Destroy(other.gameObject);
            }
        }
        
        private void Jump()
        {
            if (!_isGrounded) return;
            _isGrounded = false;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            if (_rigidbody.velocity.magnitude > _maxVerticalSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _maxVerticalSpeed;
            }
            TriggerAnimation(CharacterAnimations.Jump);
        }

        private void RunLeft()
        {
            transform.position += Vector3.left * (_runSpeed * Time.deltaTime);
            if (!_isGrounded) return;
            TriggerAnimation(CharacterAnimations.Run);
        }

        private void RunRight()
        {
            if (!_movementAllowed) return;
            transform.position += Vector3.right * (_runSpeed * Time.deltaTime);
            if (!_isGrounded) return;
            TriggerAnimation(CharacterAnimations.Run);
        }

        private void TriggerAnimation(CharacterAnimations animation)
        {
            if (_currentAnimation == animation) return;
            _animator.SetTrigger(animation.ToString());
            _currentAnimation = animation;
        }
    }
}
