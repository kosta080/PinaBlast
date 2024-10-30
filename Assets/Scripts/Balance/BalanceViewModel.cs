using Kor.Infra;

namespace Kor.Balance
{
    public class BalanceViewModel
    {
        private BalanceModel _balanceModel;
        
        public ReactiveProperty<int> BalanceCash => _balanceModel.BalanceCash;
        public ReactiveProperty<int> BalanceEnergy => _balanceModel.BalanceEnergy;

        public BalanceViewModel(BalanceModel balanceModel)
        {
            _balanceModel = balanceModel;
        }

        public void AddCash(int amount)
        {
            BalanceCash.Value += amount;
        }

        public void AddEnergy(int amount)
        {
            BalanceEnergy.Value += amount;
        }
    }
}