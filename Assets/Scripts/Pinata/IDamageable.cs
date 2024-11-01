using UnityEngine;

namespace Kosta.Pinata
{
    public interface IDamageable
    {
        public void TakeDamage(int damage, Vector3 transformPosition, float shockForce);
    }
}