using UnityEngine;

namespace Kosta
{
    public class Shocker : MonoBehaviour, IPinataTool
    {
        [SerializeField] private float shockForce = 10f;
        public void DoEffect(Rigidbody2D effected)
        {
            Vector3 awayDir = effected.transform.position - transform.position;
            Vector2 force = new Vector2(awayDir.x, awayDir.y).normalized * shockForce * effected.mass;
            effected.AddForce(force, ForceMode2D.Impulse);
        }
    }

    public interface IPinataTool
    {
        public void DoEffect(Rigidbody2D effected);
    }
}
