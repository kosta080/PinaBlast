using UnityEngine;

namespace Kosta.Player
{
    [CreateAssetMenu(fileName = "PlayerAttack", menuName = "PinaBlast/PlayerAttack")]
    public class PlayerAttack : ScriptableObject
    {
        public GameObject ImpactTargetEffect;
        public GameObject ImpactWallEffect;
        public Vector3 EffectScaleBig;
        public Vector3 EffectScaleSmall;
        public float ShockForce = 5f;
        public int Damage = 10;
        public float CooldownTime = 0.5f;
    }
}