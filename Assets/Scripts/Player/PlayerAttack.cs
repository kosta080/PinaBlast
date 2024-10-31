using System;
using System.Collections;
using Infra;
using Kosta.Controls;
using UnityEngine;
using Kosta.Infra;

namespace Kosta.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private Transform _aimDots;
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private Animator _bulletAnimator;
        [SerializeField] private Transform _explosionsContainer;
        [SerializeField] private PlayerAttackSpecs playerAttackSpecs;

        private SpriteRenderer[] _dotSprites;
        private bool _cooling = false;
        private bool _buttonReleased = true;

        private Vector3 _characterScaleOriginal;
        private Vector3 _characterScaleFlip;
        private PlayerController _playerController;
        private bool _attackAllowed = true;
        
        private const string ShootAnimationName = "Shoot";
        private const float FlopCharacterAngle = 90f;
        private const float RaycastDistanceFromCamera = 10f;
        
        private EventManager _eventManager;

        private void Awake()
        {
            _characterScaleOriginal = _characterRoot.localScale;
            _characterScaleFlip = _characterScaleOriginal;
            _characterScaleFlip.x *= -1;
            
            _dotSprites = _aimDots.GetComponentsInChildren<SpriteRenderer>();
        }
        
        private void Start()
        {
            _playerController = ServiceLocator.Resolve<PlayerController>();
            _eventManager = ServiceLocator.Resolve<EventManager>();

            _eventManager.OnTimeIsUp += DisableAttack;
            _eventManager.OnPinataExploded += DisableAttack;
            _eventManager.OnRestartRound += EnableAttack;
        }

        private void Update()
        {
            TryAim();
            if (_playerController.IsKeyDown(KeyCode.LeftControl))
            {
                TryShoot();
                _buttonReleased = false;
                return;
            }

            _buttonReleased = true;
         
            ResetDotsColor();
        }

        private void TryAim()
        {
            float angle = Mathf.Atan2(_aimTarget.position.y - transform.position.y, _aimTarget.position.x - transform.position.x) * Mathf.Rad2Deg;
            _aimDots.localRotation = Quaternion.Euler(0f, 0f, angle);
            _characterRoot.localScale = angle > FlopCharacterAngle ? _characterScaleFlip : _characterScaleOriginal;
        }

        private void TryShoot()
        {
            if (_cooling || !_attackAllowed || !_buttonReleased) return;
            SetDotsColor(Color.red);
            RaycastShot();
        }

        private void RaycastShot()
        {
            _cooling = true;
            StartCoroutine(StartCooldown());
            
            _bulletAnimator.SetTrigger(ShootAnimationName);

            Vector2 direction = _aimDots.right;
            Vector2 startPosition = transform.position;
            RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, direction, RaycastDistanceFromCamera);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag(GlobalValues.TagPlayer)) continue;

                var damageable = hit.collider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(playerAttackSpecs.Damage, transform.position, playerAttackSpecs.ShockForce);
                    CreateImpactEffect(playerAttackSpecs.ImpactTargetEffect, hit.point, true);
                }
                else
                {
                    CreateImpactEffect(playerAttackSpecs.ImpactWallEffect, hit.point, false);
                }
                break;
            }
        }

        private IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(playerAttackSpecs.CooldownTime);
            _cooling = false;
        }

        private void SetDotsColor(Color color)
        {
            foreach (var dot in _dotSprites)
            {
                dot.color = color;
            }
        }

        private void ResetDotsColor()
        {
            SetDotsColor(Color.white);
        }

        private void CreateImpactEffect(GameObject prefab, Vector3 position, bool isBig)
        {
            var impactEffect = Instantiate(prefab, position, Quaternion.identity, _explosionsContainer);
            impactEffect.transform.localScale = isBig ? playerAttackSpecs.EffectScaleBig : playerAttackSpecs.EffectScaleSmall;
        }
        
        private void OnDestroy()
        {
            _eventManager.OnTimeIsUp -= DisableAttack;
            _eventManager.OnPinataExploded -= DisableAttack;
            _eventManager.OnRestartRound -= EnableAttack;
        }

        private void EnableAttack()
        {
            _attackAllowed = true;
        }
        
        private void DisableAttack()
        {
            _attackAllowed = false;
        }
    }
}
