using Kosta.Infra;
using Kosta.Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Kosta.Controls
{
    public class UIJoystick : MonoBehaviour
    {
        [SerializeField] private Button _buttonUp;
        [SerializeField] private Button _buttonRight;
        [SerializeField] private Button _buttonLeft;
        [SerializeField] private Button _buttonFire;
        
        private PlayerController _playerController;

        private void Start()
        {
            AddButtonListeners(_buttonUp, KeyCode.UpArrow);
            AddButtonListeners(_buttonRight, KeyCode.RightArrow);
            AddButtonListeners(_buttonLeft, KeyCode.LeftArrow);
            AddButtonListeners(_buttonFire, KeyCode.LeftControl);
   
            _playerController = ServiceLocator.Resolve<PlayerController>();
        }

        private void AddButtonListeners(Button button, KeyCode keyCode)
        {
            var eventTrigger = button.gameObject.AddComponent<EventTrigger>();
            var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            pointerDown.callback.AddListener((_) =>
            {
                _playerController.OnKeyPress?.Invoke(keyCode);
            });
            eventTrigger.triggers.Add(pointerDown);
            var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
            pointerUp.callback.AddListener((_) =>
            {
                _playerController.OnKeyRelease?.Invoke(keyCode);
            });
            eventTrigger.triggers.Add(pointerUp);
        }
    }
}