using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    // todo Bishop
    public class Bishop : Piece
    {
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
        }

        public override bool CanMoveTo(Square toSquare)
        {
            if (toSquare.Color != Color) return false;
            if (Loop(toSquare, 1, 1)) return true;
            if (Loop(toSquare, 1, -1)) return true;
            if (Loop(toSquare, -1, 1)) return true;
            if (Loop(toSquare, -1, -1)) return true;

            return false;
        }

        private bool Loop(Square toSquare, int row, int col)
        {
            Square tmpSquare = new Square(currRow, currColumn);

            for (int i = 1; i < Board.Columns; i++)
            {
                if (tmpSquare.IsSame(toSquare)) return true;
                tmpSquare.Column += row;
                tmpSquare.Row += col;
            }
            return false;
        }
    }
}