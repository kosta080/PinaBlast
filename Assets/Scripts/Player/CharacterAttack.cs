using System.Collections;
using Infra;
using Kosta.Controls;
using UnityEngine;
using Kor.Infra;

namespace Kosta.Player
{
    public class CharacterAttack : MonoBehaviour
    {
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private Transform _aimDots;
        [SerializeField] private Transform _characterRoot;
        [SerializeField] private Animator _bulletAnimator;
        [SerializeField] private Transform _explosionsContainer;
        [SerializeField] private PlayerAttack _playerAttack;

        private SpriteRenderer[] _dotSprites;
        private bool _cooling = false;

        private Vector3 _characterScaleOriginal;
        private Vector3 _characterScaleFlip;
        private PlayerController _playerController;
        
        private const string ShootAnimationName = "Shoot";
        private const float FlopCharacterAngle = 90f;
        private const float RaycastDistanceFromCamera = 10f;

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
        }

        private void Update()
        {
            TryAim();
            if (_playerController.IsKeyDown(KeyCode.LeftControl))
            {
                TryShoot();
                return;
            }
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
            if (_cooling) return;

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
                    damageable.TakeDamage(_playerAttack.Damage, transform.position, _playerAttack.ShockForce);
                    CreateImpactEffect(_playerAttack.ImpactTargetEffect, hit.point, true);
                }
                else
                {
                    CreateImpactEffect(_playerAttack.ImpactWallEffect, hit.point, false);
                }
                break;
            }
        }

        private IEnumerator StartCooldown()
        {
            yield return new WaitForSeconds(_playerAttack.CooldownTime);
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
            impactEffect.transform.localScale = isBig ? _playerAttack.EffectScaleBig : _playerAttack.EffectScaleSmall;
        }
    }
}