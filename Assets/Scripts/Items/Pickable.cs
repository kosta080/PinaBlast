using System.Collections;
using UnityEngine;

namespace Kosta.Items
{
    public class Pickable : MonoBehaviour, IPickable
    {
        public PickableData PickableData;
        
        public PickableData GetPickableData()
        {
            return PickableData;
        }

        private void Start()
        {
            StartCoroutine(Decay());
        }

        private IEnumerator Decay()
        {
            yield return new WaitForSeconds(PickableData.DecayTime);
            Destroy(gameObject);
        }
    }
}
