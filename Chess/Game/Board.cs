using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    internal class Board
    {
        private static Board instance;
        public static readonly int TotalRows = 8, TotalColumns = 8;
        private Square[,] squares = new Square[TotalRows, TotalColumns];
        public Square[,] Squares => squares;

        public Board()
        {
            InitSquares();
        }

        private void InitSquares()
        {
            for (int row = 0; row < TotalRows; row++)
            {
                bool evenRow = (row%2 == 0);
                for (int column = 0; column < TotalColumns; column++)
                {
                    bool evenColumn = (column%2 == 0);

                    Enums.Color color;
                    if (evenRow && evenColumn || !evenRow && !evenColumn)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    squares[row, column] = new Square(row + 1, column + 1, color); // start at row and column 1
                }
            }
        }

        public void AddPieceToSquare(Piece piece, int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square != null) square.CurrPiece = piece;
        }

        public Square GetSquare(int row, int column)
        {
            int i = row - 1;
            int j = column - 1;

            if (i >= 0 && j >= 0 && i < TotalRows && j < TotalColumns)
                return squares[i, j];

            return null;
        }
    }
}