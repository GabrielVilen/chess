using Chess.Logic;
using Chess.Util;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(Enums.Color color) : base(Enums.PieceType.Knight, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Knight_black : Unicodes.Knight_white);
        }
        
        public override bool CanMoveTo(Square square)
        {
            return IsMatch(square, 2, 1) || IsMatch(square, 1, 2);
        }

        protected override bool IsMatch(Square square, int row, int column)
        {
            int destRow = square.Row;
            int toCol = square.Column;

            if ((destRow == currRow + row) && (toCol == currColumn + column)) return true;
            if ((destRow == currRow + row) && (toCol == currColumn - column)) return true;
            if ((destRow == currRow - row) && (toCol == currColumn + column)) return true;
            if ((destRow == currRow - row) && (toCol == currColumn - column)) return true;

            return false;
        }
    }
}