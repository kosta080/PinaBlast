using Kosta.UI;

namespace Kosta.Round
{
    public class RoundManager
    {
        private PopupManager _popupManager; 
        public RoundManager(PopupManager popupManager)
        {
            _popupManager = popupManager;
        } 
        
        public void StartRound() { }
        public void PinataExploded() { }

        public void TimeIsUp()
        {
            _popupManager.ShowTimeIsUpPopup();
        }
    }
}