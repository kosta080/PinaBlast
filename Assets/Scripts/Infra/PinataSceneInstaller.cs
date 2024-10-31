using Infra;
using Kor.Balance;
using Kor.Infra;
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
            ServiceLocator.Register(new TimerController());
            ServiceLocator.Register(new BalanceViewModel(new BalanceModel()), true);
            ServiceLocator.Register(new PlayerController());
            ServiceLocator.Register(new PickingManager());
        }
        
        void Start()
        {
            Application.targetFrameRate = GlobalValues.targetFrameRate;
            QualitySettings.vSyncCount = GlobalValues.vSyncCount;
        }
        
    }
}
