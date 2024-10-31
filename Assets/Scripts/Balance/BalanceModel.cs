using Kosta.Infra;

namespace Kosta.Balance
{
    public class BalanceModel
    {
        public ReactiveProperty<int> BalanceCash = new(0);
        public ReactiveProperty<int> BalanceEnergy = new(0);
    }
}