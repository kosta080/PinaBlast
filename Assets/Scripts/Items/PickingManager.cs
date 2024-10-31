using Kosta.Balance;
using Kosta.Infra;
using UnityEngine;

namespace Kosta.Items
{
    public class PickingManager
    {
        private BalanceViewModel _balanceViewModel;
        public PickingManager()
        {
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
        }
        
        public void HandlePicking(Collider2D otherCollider)
        {
            var pickable = otherCollider.GetComponent<IPickable>();
            if (pickable == null) return;
            var pickableData = pickable.GetPickableData();
            
            _balanceViewModel.AddEnergy(pickableData.EnergyAmount);
            _balanceViewModel.AddCash(pickableData.CashAmount);
        }
    }
}