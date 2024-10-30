using UnityEngine;
using Kor.Infra;
using Kor.Balance;
using Kor.Board;
using Kor.BuySell;

namespace Kor.Initialization
{
    public class Bootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Register(new EventManager(), true);
            ServiceLocator.Register(new BalanceViewModel(new BalanceModel()), true);
            ServiceLocator.Register(new BoardViewModel(new BoardModel()), true);
            ServiceLocator.Register(new BuySellViewModel(), true);
        }
    }
}