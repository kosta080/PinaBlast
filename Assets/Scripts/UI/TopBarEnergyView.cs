using Kosta.Balance;
using Kosta.Infra;
using TMPro;
using UnityEngine;

namespace Kosta.UI
{
    public class TopBarEnergyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textField;
        
        private BalanceViewModel _balanceViewModel;
        private void Start()
        {
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
            _balanceViewModel.BalanceEnergy.ValueChanged += OnBalanced;
            _textField.text = _balanceViewModel.BalanceEnergy.Value.ToString();
        }
        
        private void OnBalanced(int newAmount)
        {
            _textField.text = newAmount.ToString();
        }
        
    }
}