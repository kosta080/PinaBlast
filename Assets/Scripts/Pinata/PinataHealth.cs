using Kosta.Infra;
using Kosta.Round;
using UnityEngine;

namespace Kosta
{
    public class PinataHealth
    {
        public int HealthMax = 300;
        public ReactiveProperty<int> HealthPoints = new();

        private EventManager _eventManager;
        public PinataHealth()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            HealthPoints.Value = HealthMax;

            _eventManager.OnRestartRound += ResetHealth;
        }
        
        public void ReduceHealth(int value)
        {
            if (HealthPoints.Value > 0)
            {
                HealthPoints.Value -= value;
                return;
            }

            if (_eventManager == null)
            {
                _eventManager = ServiceLocator.Resolve<EventManager>();
            }
            Debug.Log("Health is 0");
            _eventManager.OnPinataExploded?.Invoke();
        }

        private void ResetHealth()
        {
            HealthPoints.Value = HealthMax;
        }
    }
}