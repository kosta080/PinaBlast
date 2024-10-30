
namespace Kor.Board
{
    public class BoardModel
    {
        private const int Size = 5;
    
        public BoardState UserBoardState { get; private set; }

        public BoardModel()
        {
            UserBoardState = new BoardState(Size, Size);
        }
        
        public void UpdateCell(int column, int row, CellState cellState)
        {
            if (column >= 0 && column < UserBoardState.Board.Length &&
                row >= 0 && row < UserBoardState.Board[column].Length)
            {
                UserBoardState.Board[column][row] = cellState;
            }
        }

        public void ResetBoard()
        {
            for (int i = 0; i < UserBoardState.Board.Length; i++)
            {
                for (int j = 0; j < UserBoardState.Board[i].Length; j++)
                {
                    UserBoardState.Board[i][j] = CellState.Empty;
                }
            }
        }
    }

    public class BoardState
    {
        public CellState[][] Board { get; }

        public BoardState(int width, int height)
        {
            Board = new CellState[width][];
            for (int i = 0; i < width; i++)
            {
                Board[i] = new CellState[height];
                for (int j = 0; j < height; j++)
                {
                    Board[i][j] = CellState.Empty;
                }
            }
        }
    }
}