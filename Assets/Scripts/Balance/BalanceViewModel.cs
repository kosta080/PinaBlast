using Kosta.Infra;

namespace Kosta.Balance
{
    public class BalanceViewModel
    {
        private BalanceModel _balanceModel;
        
        public ReactiveProperty<int> BalanceCash => _balanceModel.BalanceCash;
        public ReactiveProperty<int> BalanceEnergy => _balanceModel.BalanceEnergy;

        public RoundScore CurrentRoundScore = new RoundScore();
       
        private EventManager _eventManager;

        public BalanceViewModel(BalanceModel balanceModel)
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _balanceModel = balanceModel;

            _eventManager.OnRestartRound += OnRestartRound;
            _eventManager.OnTimeIsUp += OnTimeIsUp;

        }
        
        private void OnRestartRound()
        {
            CurrentRoundScore = new RoundScore();
        }
        
        private void OnTimeIsUp()
        {
            BalanceCash.Value -= CurrentRoundScore.Cash;
            BalanceEnergy.Value -= CurrentRoundScore.Energy;
        }

        public void AddCash(int amount)
        {
            if (amount == 0) return;
            BalanceCash.Value += amount;
            CurrentRoundScore.AddCash(amount);
        }

        public void AddEnergy(int amount)
        {
            if (amount == 0) return;
            BalanceEnergy.Value += amount;
            CurrentRoundScore.AddEnergy(amount);
        }
    }

    public class RoundScore
    {
        public int Cash = 0;
        public int Energy = 0;

        public RoundScore()
        {
            Cash = 0;
            Energy = 0;
        }
        public void AddCash(int amount)
        {
            if (amount == 0) return;
            Cash += amount;
        }

        public void AddEnergy(int amount)
        {
            if (amount == 0) return;
            Energy += amount;
        }
    }
}