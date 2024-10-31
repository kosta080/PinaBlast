using System;

namespace Kosta.Infra
{
    public class EventManager
    {
        public Action OnTimeIsUp;
        public Action OnPinataExploded;
        public Action OnRestartRound;
        public Action OnFinalSpawnFinished;
        
        public Action AfterPickCoin;
        public Action AfterPickEnergy;
        public Action AfterPlayerShot;
    }
    
}