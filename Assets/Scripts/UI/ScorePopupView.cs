using Infra;
using Kosta.Balance;
using Kosta.Infra;
using Kosta.Timer;
using TMPro;
using UnityEngine;

namespace Kosta.UI
{
    public class ScorePopupView : PopupView
    {
        [SerializeField] private CommonButton _playAgainButton;
        [SerializeField] private TMP_Text _ScoreTextCash;
        [SerializeField] private TMP_Text _ScoreTextEnergy;
        [SerializeField] private TMP_Text _timeLeftTextEnergy;
        
        private EventManager _eventManager;
        private BalanceViewModel _balanceViewModel;
        private TimerController _timerController;
        
        private void Start()
        {
            _eventManager = ServiceLocator.Resolve<EventManager>();
            _balanceViewModel = ServiceLocator.Resolve<BalanceViewModel>();
            _timerController = ServiceLocator.Resolve<TimerController>();
            
            if (_eventManager == null) Debug.LogError("Event manager is null");
            if (_balanceViewModel == null) Debug.LogError("Balance view model is null");
            if (_timerController == null) Debug.LogError("Timer controller is null");
            
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
            _timeLeftTextEnergy.text = $"{_timerController.WholeSecondsLeft} Seconds left";
        }

        private void OnClickPlayAgainButton()
        {
            _eventManager.OnRestartRound?.Invoke();
        }
    }
}