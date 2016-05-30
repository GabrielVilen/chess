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

        public override bool CanMoveTo(Square clickedSquare)
        {
            if (clickedSquare.Color != Color) return false;
            if (clickedSquare.Row > currRow) return IsMatch(clickedSquare, 1, 1)  || IsMatch(clickedSquare, 1, -1);
            if (clickedSquare.Row < currRow) return IsMatch(clickedSquare, -1, 1) || IsMatch(clickedSquare, -1, -1);

            return false;
        }
    }
}