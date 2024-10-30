using System.Collections.Generic;
using Kor.Infra;
using Kor.Tools;
using UnityEngine;

namespace Kor.Board
{
    public class BoardView : MonoBehaviour
    {

        [RequireReference] [SerializeField] private GameObject _greenTurnText;
        [RequireReference] [SerializeField] private GameObject _redTurnText;
        [SerializeField] private List<ColumnView> columnsViews;
        private BoardViewModel _boardViewModel;

        public void Start()
        {
            _boardViewModel = ServiceLocator.Resolve<BoardViewModel>();
            _boardViewModel.OnBoardChanged += OnBoardChanged;
            _boardViewModel.OnTurnChanged += UpdateTurnTextActive;
            SetClickListeners();
            UpdateTurnTextActive(true);
        }

        private void UpdateTurnTextActive(bool green)
        {
            _greenTurnText.SetActive(green);
            _redTurnText.SetActive(!green);
        }

        private void SetClickListeners()
        {
            for (var i = 0; i < columnsViews.Count; i++)
            {
                int columnIndex = i;
                columnsViews[i].OnButtonClick += (int rowIndex) =>
                {
                    _boardViewModel.HandleCellClick(columnIndex, rowIndex);
                };
            }
        }
        
        private void OnBoardChanged(BoardState boardState)
        {
            if (boardState.Board.Length != columnsViews.Count)
            {
                Debug.LogError("Mismatch between board data and column views count.");
                Debug.Log($"{boardState.Board.Length} {columnsViews.Count}");
                return;
            }

            for (int colIndex = 0; colIndex < boardState.Board.Length; colIndex++)
            {
                if (colIndex < columnsViews.Count && columnsViews[colIndex] != null)
                {
                    columnsViews[colIndex].Apply(boardState.Board[colIndex]);
                }
                else
                {
                    Debug.LogError($"Column view at index {colIndex} is missing or null.");
                }
            }
        }

        private void OnDestroy()
        {
            if (_boardViewModel != null)
            {
                _boardViewModel.OnBoardChanged -= OnBoardChanged;
            }
        }
    }
}