using Infra;
using Kor.Infra;
using UnityEngine;

namespace Kor.UI
{
    public class TutorialPopupView : PopupView
    {
        [SerializeField] private CommonButton _actionButton;

        EventManager _eventManager;
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _actionButton.onClick.AddListener(ActionButtonClicked);
        }

        private void ActionButtonClicked()
        {
            _eventManager.OnRestartRound?.Invoke();
        }
    }
}