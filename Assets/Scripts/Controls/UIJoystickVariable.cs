using Kosta.Controls;
using Kosta.Infra;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controls
{
    public class UIJoystickVariable : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField, Range(0f, 1f)] private float _joystickThresholdHorizontal = 0.15f;
        [SerializeField, Range(0f, 1f)] private float _joystickThresholdVertical = 0.35f;
        
        private PlayerController _playerController;
        private bool _isRightPressed, _isLeftPressed, _isUpPressed;

        private void Start()
        {
            _playerController = ServiceLocator.Resolve<PlayerController>();
        }

        private void Update()
        {
            HandleHorizontalInput();
            HandleVerticalInput();
        }

        private void HandleVerticalInput()
        {
            if (_joystick.Vertical > _joystickThresholdVertical)
            {
                _isUpPressed = true;
                _playerController.OnKeyPress?.Invoke(KeyCode.UpArrow);
            }
            else if (_isUpPressed)
            {
                _isUpPressed = false;
                _playerController.OnKeyRelease?.Invoke(KeyCode.UpArrow);
            }
        }

        private void HandleHorizontalInput()
        {
            if (_joystick.Horizontal > _joystickThresholdHorizontal)
            {
                _isRightPressed = true;
                _playerController.OnKeyPress?.Invoke(KeyCode.RightArrow);
            }
            else if (_isRightPressed)
            {
                _isRightPressed = false;
                _playerController.OnKeyRelease?.Invoke(KeyCode.RightArrow);
            }

            if (_joystick.Horizontal < -_joystickThresholdHorizontal)
            {
                _isLeftPressed = true;
                _playerController.OnKeyPress?.Invoke(KeyCode.LeftArrow);
            }
            else if (_isLeftPressed)
            {
                _isLeftPressed = false;
                _playerController.OnKeyRelease?.Invoke(KeyCode.LeftArrow);
            }
        }
    }
}