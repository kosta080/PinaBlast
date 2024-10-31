using Infra;
using Kor.Infra;
using UnityEngine;

namespace Kor.UI
{
    public class TimeIsUpPopupView : PopupView
    {
        [SerializeField] private CommonButton _tryAgainButton;

        EventManager _eventManager;
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            if (_eventManager == null)
            {
                Debug.LogError("Event manager is null");
            }
            if (_tryAgainButton == null)
            {
                Debug.LogError("Play Again button is null!");
                return;
            }
            _tryAgainButton.onClick.AddListener(OnClickTryAgainButton);
        }

        private void OnClickTryAgainButton()
        {
            _eventManager.OnRestartRound?.Invoke();
        }
    }
}