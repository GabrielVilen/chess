using Chess.Logic;
using Chess.Util;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Bishop_black : Unicodes.Bishop_white);
        }

        public override bool CanMoveTo(Square square)
        {
            if (square.Row > currRow) return IsMatch(square, 1, 1)  || IsMatch(square, 1, -1);
            if (square.Row < currRow) return IsMatch(square, -1, 1) || IsMatch(square, -1, -1);

            return false;
        }
    }
}