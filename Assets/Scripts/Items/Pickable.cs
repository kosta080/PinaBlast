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
    }
}
