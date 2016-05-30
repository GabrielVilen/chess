using Chess.Logic;
using Chess.Util;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(Enums.Color color) : base(Enums.PieceType.Rook, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Rook_black : Unicodes.Rook_white);
        }

        public override bool CanMoveTo(Square clickedSquare)
        {
            int destColumn = clickedSquare.Column;
            int destRow = clickedSquare.Row;

            if (destColumn == currColumn)
            {
                if (destRow > currRow) return IsMatch(clickedSquare, 1, 0);
                if (destRow < currRow) return IsMatch(clickedSquare, -1, 0);
            }
            else if (destRow == currRow)
            {
                if (destColumn > currColumn) return IsMatch(clickedSquare, 0, 1);
                if (destColumn < currColumn) return IsMatch(clickedSquare, 0, -1);
            }

            return false;
        }
    }
}