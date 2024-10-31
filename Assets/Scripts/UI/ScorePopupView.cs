using Infra;
using Kosta.Balance;
using Kosta.Infra;
using TMPro;
using UnityEngine;

namespace Kosta.UI
{
    public class ScorePopupView : PopupView
    {
        [SerializeField] private CommonButton _playAgainButton;
        [SerializeField] private TMP_Text _ScoreTextCash;
        [SerializeField] private TMP_Text _ScoreTextEnergy;
        
        EventManager _eventManager;
        BalanceViewModel _balanceViewModel;
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
            if (_eventManager == null)
            {
                Debug.LogError("Event manager is null");
            }

            if (_balanceViewModel == null)
            {
                Debug.LogError("Balance view model is null");
            }
            if (_playAgainButton == null)
            {
                Debug.LogError("Play Again button is null!");
                return;
            }
            _playAgainButton.onClick.AddListener(OnClickPlayAgainButton);

            SetScoreText();
        }

        private void SetScoreText()
        {
            RoundScore roundScore = _balanceViewModel.CurrentRoundScore;
            _ScoreTextCash.text = roundScore.Cash.ToString();
            _ScoreTextEnergy.text = roundScore.Energy.ToString();
        }

        private void OnClickPlayAgainButton()
        {
            _eventManager.OnRestartRound?.Invoke();
        }
    }
}