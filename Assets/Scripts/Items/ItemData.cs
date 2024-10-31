using UnityEngine;

namespace Kosta.Items
{
    [CreateAssetMenu(fileName = "PickableData", menuName = "PinaBlast/PickableData")]
    public class PickableData : ScriptableObject
    {
        public int CashAmount;
        public int EnergyAmount;
        
        public float DecayTime;
    }
}