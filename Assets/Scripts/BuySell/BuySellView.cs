using Kosta.Infra;
using Kosta.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Kosta.BuySell
{
    public class BuySellView : MonoBehaviour
    {
        [RequireReference] [SerializeField] private Button _buyButton;
        [RequireReference] [SerializeField] private Button _sellButton;

        private BuySellViewModel _buySellViewModel;
        

        public void Start()
        {
            _buySellViewModel = ServiceLocator.Resolve<BuySellViewModel>();
    
            if (_buySellViewModel == null)
            {
                Debug.LogError("BuySellViewModel could not be resolved.");
                return;
            }
            
            _buyButton.onClick.AddListener(_buySellViewModel.PerformBuy);
            _sellButton.onClick.AddListener(_buySellViewModel.PerformSell);
        }
    }
}