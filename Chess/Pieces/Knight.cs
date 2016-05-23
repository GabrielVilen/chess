using Chess.Gui;
using Chess.Util;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(Enums.Color color) : base(Enums.PieceType.Knight, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Knight_black : Unicodes.Knight_white);
        }
        
        public override bool CanMoveTo(Square destSquare)
        {
            return IsMatch(destSquare, 2, 1) || IsMatch(destSquare, 1, 2);
        }

        protected override bool IsMatch(Square destSquare, int row, int column)
        {
            int destRow = destSquare.Row;
            int toCol = destSquare.Column;

            if ((destRow == currRow + row) && (toCol == currColumn + column)) return true;
            if ((destRow == currRow + row) && (toCol == currColumn - column)) return true;
            if ((destRow == currRow - row) && (toCol == currColumn + column)) return true;
            if ((destRow == currRow - row) && (toCol == currColumn - column)) return true;

            return false;
        }
    }
}