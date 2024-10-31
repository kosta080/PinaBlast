using System;
using Kosta.Infra;
using UnityEngine;

namespace Kosta
{
    public class PinataController : MonoBehaviour, IDamageable
    {
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PickableSpawner _pickableSpawner;
        [SerializeField] private Animator _pinataAnimator;

        private PinataHealth _pinataHealth;
        private EventManager _eventManager;
        
        private string PinataResetAnimation = "Reset";
        private string PinataExploadAnimation = "Expload";

        private void Start()
        {
            _pinataHealth = ServiceLocator.Resolve<PinataHealth>();
            _eventManager = ServiceLocator.Resolve<EventManager>();

            _eventManager.OnPinataExploded += OnPinataExploded;
            _eventManager.OnRestartRound += OnPinataRestart;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var tool = other.transform.gameObject.GetComponent<IPinataTool>();
            if (tool == null) return;
            tool.DoEffect(_rigidbody2D);
        }

        public void TakeDamage(int damage, Vector3 transformPosition, float shockForce)
        {
            Vector3 awayDir = transform.position - transformPosition;
            Vector2 force = new Vector2(awayDir.x, awayDir.y).normalized * shockForce;
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            _pickableSpawner.SpawnPickable(PickableSpawner.SpawnType.Random);
            _pinataHealth.ReduceHealth(damage);
        }
        
        private void OnPinataRestart()
        {
            
            _pinataAnimator.SetTrigger(PinataResetAnimation);
        }

        [ContextMenu("PinataExploadAnimation")]
        private void OnPinataExploded()
        {
            _pinataAnimator.SetTrigger(PinataExploadAnimation);
        }
    }

    public interface IDamageable
    {
        public void TakeDamage(int damage, Vector3 transformPosition, float shockForce);
    }
}
