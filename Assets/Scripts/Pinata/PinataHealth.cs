using Kor.Infra;
using UnityEngine;

namespace Kosta
{
    public class PinataHealth
    {
        public int HealthMax = 500;
        public ReactiveProperty<int> HealthPoints = new();

        public PinataHealth()
        {
            HealthPoints.Value = HealthMax;
        }
        
        public void ReduceHealth(int value)
        {
            Debug.Log($"ReduceHealth: {value}");    
            Debug.Log($"HP: {HealthPoints.Value}");    
            if (HealthPoints.Value > 0)
            {
                HealthPoints.Value -= value;
            }
            else
            {
                Debug.Log("Health is 0");
            }
            Debug.Log($"HP: {HealthPoints.Value}"); 
        }

        private void ResetHealth()
        {
            HealthPoints.Value = HealthMax;
        }
    }
}