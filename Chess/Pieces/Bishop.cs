using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    internal class Bishop : Piece
    {
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
        }

        public override bool CanMoveTo(Square destSquare)
        {
            if (destSquare.Color != Color) return false;
            if (destSquare.Row > currRow) return IsMatch(destSquare, 1, 1)  || IsMatch(destSquare, 1, -1);
            if (destSquare.Row < currRow) return IsMatch(destSquare, -1, 1) || IsMatch(destSquare, -1, -1);

            return false;
        }
    }
}