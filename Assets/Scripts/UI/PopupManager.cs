using Kor.Balance;
using Kor.Infra;
using Kor.Tools;
using UnityEngine;

namespace Kor.UI
{
    public class PopupManager : MonoBehaviour
    {
        [RequireReference] [SerializeField] private GameObject popupPrefab;
        [RequireReference] [SerializeField] private Transform popupContainer;
        private GameObject _popupInstance;
        private EventManager _eventManager;
        private BalanceViewModel _balanceViewModel => ServiceLocator.Resolve<BalanceViewModel>();

    
        public void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _eventManager.OnCollisionWithObject += HandleCollisionWithObject;
        }

        private void HandleCollisionWithObject(string collisionMessage)
        {
            if (_popupInstance == null)
                _popupInstance = Instantiate(popupPrefab, popupContainer);

            IPopupView popup = _popupInstance.GetComponent<IPopupView>();
            if (popup != null)
            {
                string fullMessage = collisionMessage + "\nCurrent Balance: " + _balanceViewModel.BalanceCash.Value;
                popup.Init(fullMessage);
            }
        }

        private void OnDestroy()
        {
            if (_eventManager != null)
                _eventManager.OnCollisionWithObject -= HandleCollisionWithObject;
        }
    }
}