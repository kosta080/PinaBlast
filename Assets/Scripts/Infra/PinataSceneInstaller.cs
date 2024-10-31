using Infra;
using Kosta.Balance;
using Kosta.Infra;
using UnityEngine;
using Kosta.Controls;
using Kosta.Items;
using Kosta.Timer;

namespace Kosta
{
    public class PinataSceneInstaller : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Register(new EventManager(),true);
            ServiceLocator.Register(new TimerController(),true);
            ServiceLocator.Register(new BalanceViewModel(new BalanceModel()), true);
            ServiceLocator.Register(new PlayerController());
            ServiceLocator.Register(new PickingManager());
            ServiceLocator.Register(new PinataHealth());
        }
        
        void Start()
        {
            Application.targetFrameRate = GlobalValues.targetFrameRate;
            QualitySettings.vSyncCount = GlobalValues.vSyncCount;
            
        }
        
    }
}
