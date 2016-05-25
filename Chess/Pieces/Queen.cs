using Chess.Logic;
using Chess.Util;

namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(Enums.Color color) : base(Enums.PieceType.Queen, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Queen_black : Unicodes.Queen_white);
        }

        public override bool CanMoveTo(Square destSquare)
        {
            int destColumn = destSquare.Column;
            int destRow = destSquare.Row;

            // loop horizontal or vertical
            if (destColumn == currColumn)
            {
                if (destRow > currRow) return IsMatch(destSquare, 1, 0);
                if (destRow < currRow) return IsMatch(destSquare, -1, 0);
            }
            else if (destRow == currRow)
            {
                if (destColumn > currColumn) return IsMatch(destSquare, 0, 1);
                if (destColumn < currColumn) return IsMatch(destSquare, 0, -1);
            }

            // loop diagonal
            if (destRow > currRow) return IsMatch(destSquare, 1, 1) || IsMatch(destSquare, 1, -1);
            if (destRow < currRow) return IsMatch(destSquare, -1, 1) || IsMatch(destSquare, -1, -1);

            return false;
        }
    }
}