using UnityEngine;

namespace Kosta.Player
{
    public class AutoAim : MonoBehaviour
    {
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private Transform _rootObject;

        private bool flipAim = false;
        
        void Update()
        {
            if (_rootObject != null)
            {
                flipAim = _rootObject.localScale.x < 0;
            }
            TryAim();
        }
        
        private void TryAim()
        {
            float angle = Mathf.Atan2(_aimTarget.position.y - transform.position.y, 
                _aimTarget.position.x - transform.position.x) * Mathf.Rad2Deg;
            if (flipAim)
            {
                Debug.Log(Time.time);
                angle = Mathf.Atan2(transform.position.y - _aimTarget.position.y,
                    transform.position.x - _aimTarget.position.x) * Mathf.Rad2Deg;
                angle *= -1;
            }
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
