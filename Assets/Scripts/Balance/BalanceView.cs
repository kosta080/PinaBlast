using Kor.Infra;
using Kor.Tools;
using TMPro;
using UnityEngine;

namespace Kor.Balance
{
    public class BalanceView : MonoBehaviour 
    {
        [RequireReference] [SerializeField] private TMP_Text balanceText;
        private BalanceViewModel _balanceViewModel => ServiceLocator.Resolve<BalanceViewModel>();

        public void Start() 
        {
            _balanceViewModel.BalanceCash.ValueChanged += UpdateBalanceCashDisplay;
            UpdateBalanceCashDisplay(_balanceViewModel.BalanceCash.Value);
        }

        private void OnDestroy()
        {
            _balanceViewModel.BalanceCash.ValueChanged -= UpdateBalanceCashDisplay;
        }

        private void UpdateBalanceCashDisplay(int balance)
        {
            balanceText.text = balance.ToString();
        }
    }
}