using System;
using System.Diagnostics;
using System.Linq;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    public class Board
    {
        private static Board instance;
        public static readonly int Rows = 8, Columns = 8;
        private Square[,] squares = new Square[Rows, Columns];
        public Square[,] Squares => squares;

        private Board()
        {
            InitSquares();
        }

        /// <summary>
        ///     Singleton that creates new instance if current is null
        /// </summary>
        /// <returns>Get singleton</returns>
        public static Board GetInstance()
        {
            if (instance == null)
                instance = new Board();

            return instance;
        }

        private void InitSquares()
        {
            for (int row = 0; row < Rows; row++)
            {
                bool evenRow = (row%2 == 0);
                for (int column = 0; column < Columns; column++)
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

        public void Score(Piece destPiece, Enums.Color color)
        {
            // todo
            throw new System.NotImplementedException();
        }

        public void AddPieceToSquare(Piece piece, int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square != null) square.CurrPiece = piece;
        }

        public Square GetSquare(int row, int column)
        {
            return squares.Cast<Square>().FirstOrDefault(square => square.Row == row && square.Column == column);
        }
    }
}