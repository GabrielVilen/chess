using Chess.Pieces;
using Chess.Util;

namespace Chess.Board
{
    public class Board
    {
        private Square[,] squares = new Square[8, 8];
        public Square[,] Squares => squares;

        public Board()
        {
            InitSquares();
        }

        private void InitSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                bool evenCol = (i%2 == 0);
                for (int j = 0; j < 8; j++)
                {
                    bool evenRow = (j%2 == 0);

                    Enums.Color color;
                    if (evenCol && evenRow || !evenCol && !evenRow)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    squares[i, j] = new Square(Enums.PieceType.None, color);
                }
            }
        }

        public static bool IsValid(Piece piece, int[][] position)
        {
            return false;
        }
    }
}