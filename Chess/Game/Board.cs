using Chess.Pieces;
using Chess.Util;

namespace Chess.Board
{
    public class Board
    {
        public static readonly int ROWS = 8, COLS = 8;
        private Square[,] squares = new Square[COLS, ROWS];
        public Square[,] Squares => squares;

        public Board()
        {
            InitSquares();
        }

        private void InitSquares()
        {
            for (int i = 0; i < COLS; i++)
            {
                bool evenCol = (i%2 == 0);
                for (int j = 0; j < ROWS; j++)
                {
                    bool evenRow = (j%2 == 0);

                    Enums.Color color;
                    if (evenCol && evenRow || !evenCol && !evenRow)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    squares[i, j] = new Square(null, color);
                }
            }
        }

        public static void Score(Piece destPiece, Enums.Color color)
        {
            // todo 


            throw new System.NotImplementedException();
        }
    }
}