using Kosta.Infra;
using Kosta.Tools;
using UnityEngine;

namespace Kosta.UI
{
    public class PopupManager : MonoBehaviour
    {
        [RequireReference] [SerializeField] private GameObject _timeIsUpPopupPrefab;
        [RequireReference] [SerializeField] private GameObject _scorePopupPrefab;
        [RequireReference] [SerializeField] private GameObject _tutorialPopupPrefab;
        
        [RequireReference] [SerializeField] private Transform popupContainer;
        
        private GameObject _popupInstance;
        private EventManager _eventManager;
    
        public void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();

            _eventManager.OnTimeIsUp += ShowTimeIsUpPopup;
            //_eventManager.OnPinataExploded += ShowScorePopup;
            _eventManager.OnRestartRound += CloseActivePopup;
            _eventManager.OnFinalSpawnFinished += ShowScorePopup;

            ShowTutorialPopup();
        }
        
        private void OnDestroy()
        {
            _eventManager.OnTimeIsUp -= ShowTimeIsUpPopup;
        }

        [ContextMenu("Close Popup")]
        private void CloseActivePopup()
        {
            Destroy(_popupInstance);
            _popupInstance = null;
        }

        [ContextMenu("ShowTimeIsUpPopup")]
        public void ShowTimeIsUpPopup()
        {
            if (_popupInstance != null) return;
            _popupInstance = Instantiate(_timeIsUpPopupPrefab, popupContainer);
        }
        
        [ContextMenu("ShowScorePopup")]
        private void ShowScorePopup()
        {
            if (_popupInstance != null) return;
            _popupInstance = Instantiate(_scorePopupPrefab, popupContainer);
        }
        
        private void ShowTutorialPopup()
        {
            _popupInstance = Instantiate(_tutorialPopupPrefab, popupContainer);
        }
    }
}