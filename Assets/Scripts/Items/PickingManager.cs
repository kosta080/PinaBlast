using Kosta.Balance;
using Kosta.Infra;
using UnityEngine;

namespace Kosta.Items
{
    public class PickingManager
    {
        private BalanceViewModel _balanceViewModel;
        
        EventManager _eventManager;
        public PickingManager()
        {
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
            _eventManager = ServiceLocator.Resolve<EventManager>();
        }
        
        public void HandlePicking(Collider2D otherCollider)
        {
            var pickable = otherCollider.GetComponent<IPickable>();
            if (pickable == null) return;
            var pickableData = pickable.GetPickableData();
            if (pickableData.CashAmount > 0) _eventManager.AfterPickCoin?.Invoke();
            if (pickableData.EnergyAmount > 0) _eventManager.AfterPickEnergy?.Invoke();
            _balanceViewModel.AddEnergy(pickableData.EnergyAmount);
            _balanceViewModel.AddCash(pickableData.CashAmount);
        }
    }
}