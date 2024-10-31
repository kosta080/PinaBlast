using UnityEngine;
using Kosta.Infra;
using Kosta.Balance;
using Kosta.Board;
using Kosta.BuySell;

namespace Kosta.Initialization
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