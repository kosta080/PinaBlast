using Kor.Infra;

namespace Kor.Balance
{
    public class BalanceModel
    {
        public ReactiveProperty<int> BalanceCash = new(0);
        public ReactiveProperty<int> BalanceEnergy = new(0);
    }
}