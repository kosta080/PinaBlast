using Kosta.Controls;
using UnityEngine;
using Kor.Infra;
using Kosta.Timer;

namespace Controls
{
    public class KeyboardInput : MonoBehaviour
    {
        private PlayerController _playerController;
        private TimerController _timerController;
        
        private KeyCode[] _supportedKeys = { KeyCode.LeftControl, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.RightArrow };
        private void Start()
        {
            _playerController = ServiceLocator.Resolve<PlayerController>();
            _timerController = ServiceLocator.Resolve<TimerController>();
        }

        private void Update()
        {
            foreach (var key in _supportedKeys)
            {
                if (Input.GetKeyDown(key))
                {
                    _playerController.OnKeyPress?.Invoke(key);
                }
                else if (Input.GetKeyUp(key))
                {
                    _playerController.OnKeyRelease?.Invoke(key);
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                _timerController.StartTimer();
            }
          
        }
    }
}