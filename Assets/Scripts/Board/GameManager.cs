using Kosta.Infra;
using UnityEngine;

namespace Kosta.Board
{
    public class GameManager : MonoBehaviour
    {
        private BoardViewModel _boardViewModel;
    
        private void Awake()
        {
            _boardViewModel = ServiceLocator.Resolve<BoardViewModel>();
        }

        public void StartGame()
        {
            _boardViewModel.StartRandomFill();
        }
    }
    
}
