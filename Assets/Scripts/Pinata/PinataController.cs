using UnityEngine;

namespace Kosta
{
    public class PinataController : MonoBehaviour, IDamageable
    {
        
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private PickableSpawner _pickableSpawner;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var tool = other.transform.gameObject.GetComponent<IPinataTool>();
            if (tool == null) return;
            tool.DoEffect(_rigidbody2D);
        }

        public void TakeDamage(float damage, Vector3 transformPosition, float shockForce)
        {
            Vector3 awayDir = transform.position - transformPosition;
            Vector2 force = new Vector2(awayDir.x, awayDir.y).normalized * shockForce;
            _rigidbody2D.AddForce(force, ForceMode2D.Impulse);
            _pickableSpawner.SpawnPickable(PickableSpawner.SpawnType.Random);
        }
    }

    public interface IDamageable
    {
        public void TakeDamage(float damage, Vector3 transformPosition, float shockForce);
    }
}
