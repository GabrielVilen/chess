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

        public override bool CanMoveTo(Square clickedSquare)
        {
            int destColumn = clickedSquare.Column;
            int destRow = clickedSquare.Row;

            // loop columns 
            if (destColumn == currColumn)
            {
                if (destRow > currRow) return IsMatch(clickedSquare,  1, 0);
                if (destRow < currRow) return IsMatch(clickedSquare, -1, 0);
            }
            // loop rows
            else if (destRow == currRow)
            {
                if (destColumn > currColumn) return IsMatch(clickedSquare, 0,  1);
                if (destColumn < currColumn) return IsMatch(clickedSquare, 0, -1);
            }

            // loop diagonal
            if (destRow > currRow) return IsMatch(clickedSquare,  1, 1) || IsMatch(clickedSquare,  1, -1);
            if (destRow < currRow) return IsMatch(clickedSquare, -1, 1) || IsMatch(clickedSquare, -1, -1);

            return false;
        }
    }
}