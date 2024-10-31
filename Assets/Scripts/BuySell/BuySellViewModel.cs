using Kosta.Balance;
using Kosta.Infra;

namespace Kosta.BuySell
{
    public class BuySellViewModel
    {
        private BalanceViewModel _balanceViewModel;
        
        public BuySellViewModel()
        {
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
        }
        
        public void PerformBuy()
        {
            _balanceViewModel.AddCash(10);
        }

        public void PerformSell()
        {
            _balanceViewModel.AddCash(-10);
        }
    }
}
