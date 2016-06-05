using Chess.Logic;
using Chess.Util;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Pieces
{
    /// <summary>
    ///     Represents a knight piece in the chess game.
    /// </summary>
    public class Knight : Piece
    {
        /// <summary>
        ///     Creates a new knight with the given color.
        /// </summary>
        public Knight(Enums.Color color) : base(Enums.PieceType.Knight, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.KnightBlack : Unicodes.KnightWhite);
        }
        
        public override bool CanMoveTo(Square square)
        {
            return IsMatch(square, 2, 1) || IsMatch(square, 1, 2);
        }

        protected override bool IsMatch(Square square, int row, int column)
        {
            int destRow = square.Row;
            int toCol = square.Column;

            if ((destRow == CurrRow + row) && (toCol == CurrColumn + column)) return true;
            if ((destRow == CurrRow + row) && (toCol == CurrColumn - column)) return true;
            if ((destRow == CurrRow - row) && (toCol == CurrColumn + column)) return true;
            if ((destRow == CurrRow - row) && (toCol == CurrColumn - column)) return true;

            return false;
        }
    }
}