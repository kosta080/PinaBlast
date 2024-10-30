using Kor.Balance;
using Kor.Infra;
using TMPro;
using UnityEngine;

namespace Kor.UI
{
    public class TopBarCashView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        
        private BalanceViewModel _balanceViewModel;
        private void Start()
        {
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
            _balanceViewModel.BalanceCash.ValueChanged += OnBalanced;
            _textField.text = _balanceViewModel.BalanceCash.Value.ToString();
        }

        private void OnBalanced(int newAmount)
        {
            _textField.text = newAmount.ToString();
        }
    }
}