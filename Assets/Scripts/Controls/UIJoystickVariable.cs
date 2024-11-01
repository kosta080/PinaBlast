using Kosta.Controls;
using Kosta.Infra;
using UnityEngine;

namespace Controls
{
    public class UIJoystickVariable : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        private float _joystickThreshold = 0.15f;
        
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = ServiceLocator.Resolve<PlayerController>();
        }

        private void Update()
        {
            if (_joystick.Horizontal > _joystickThreshold) _playerController.OnKeyPress?.Invoke(KeyCode.RightArrow);
            else _playerController.OnKeyRelease?.Invoke(KeyCode.RightArrow);
            
            if (_joystick.Horizontal < -_joystickThreshold) _playerController.OnKeyPress?.Invoke(KeyCode.LeftArrow);
            else _playerController.OnKeyRelease?.Invoke(KeyCode.LeftArrow);
            
            if (_joystick.Vertical > _joystickThreshold) _playerController.OnKeyPress?.Invoke(KeyCode.UpArrow);
            else _playerController.OnKeyRelease?.Invoke(KeyCode.UpArrow);
        }
    }
}