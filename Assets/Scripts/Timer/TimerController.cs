using System.Threading.Tasks;
using Kor.Infra;
using UnityEngine;

namespace Kosta.Timer
{
    public class TimerController
    {
        private float _roundTimeSeconds = 20f;
        private float _msLeft;
        private bool _isRunning;
        
        private const int RefreshRateMs = 16;
        
        private EventManager _eventManager;
        public TimerController()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _eventManager.OnRestartRound += RestartRound;
            _eventManager.OnPinataExploded += StopTimer;
        }

        private void RestartRound()
        {
            ResetTimer();
            StartTimer();
        }

        public void StartTimer()
        {
            if (_isRunning) return;
            ResetTimer();
            _isRunning = true;
            AsyncTimer();
        }
        
        public void StopTimer()
        {
            _isRunning = false;
        }

        private async void AsyncTimer()
        {
            while (_isRunning)
            {
                await Task.Delay(RefreshRateMs);
                _msLeft -= RefreshRateMs;
                if (_msLeft <= 0)
                {
                    _msLeft = 0;
                    StopTimer();
                    _eventManager.OnTimeIsUp?.Invoke();
                }
            }
        }

        public void ResetTimer()
        {
            _msLeft = Mathf.FloorToInt(_roundTimeSeconds * 1000f) ;
        }
        
        public int WholeSecondsLeft
        {
            get
            {
                return Mathf.FloorToInt(_msLeft/1000);
            }
        }
    }
}