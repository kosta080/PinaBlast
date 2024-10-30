using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Kor.Board
{
    public class BoardViewModel
    {
        private BoardModel _boardModel;
    
        public Action<BoardState> OnBoardChanged;
        public Action<bool> OnTurnChanged;

        private bool _green = true;
        private int _moves = 0;

        public BoardViewModel(BoardModel boardModel)
        {
            _boardModel = boardModel;
        }

        public async void StartRandomFill()
        {
            _moves = 0; 
            while (_moves < 100 && Application.isPlaying)
            {
                await Task.Delay(100);
                int column = UnityEngine.Random.Range(0, _boardModel.UserBoardState.Board.Length);
                var Facilitated = TryFacilitateColumn(column, _green ? CellState.Green : CellState.Red);
                if (Facilitated)
                {
                    SwitchTurn();
                }
                _moves++;
            }
        }

        private void SwitchTurn()
        {
            _green = !_green;
            OnTurnChanged?.Invoke(_green);
        }
        
        private int GetFreeSlotInColumn(int column)
        {
            for (int i = 0; i < _boardModel.UserBoardState.Board[column].Length; i++)
            {
                if (_boardModel.UserBoardState.Board[column][i] == CellState.Empty)
                {
                    return i;
                }
            }
            return -1;
        }
        
        private bool TryFacilitateColumn(int column, CellState cellState)
        {
            int freeSlot = GetFreeSlotInColumn(column);
            if (freeSlot < 0 || freeSlot > 5) return false;
            
            _boardModel.UpdateCell(column, freeSlot, cellState);
            OnBoardChanged?.Invoke(_boardModel.UserBoardState);
            return true;
        }

        public void HandleCellClick(int columnIndex, int rowIndex)
        {
            if (TryFacilitateColumn(columnIndex, _green ? CellState.Green : CellState.Red))
            {
                SwitchTurn();
            }
        }
    }
}
