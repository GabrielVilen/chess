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
    ///     Represents a rook piece in the chess game.
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        ///     Creates a new rook with the given color.
        /// </summary>
        public Rook(Enums.Color color) : base(Enums.PieceType.Rook, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.RookBlack : Unicodes.RookWhite);
        }

        public override bool CanMoveTo(Square square)
        {
            int destColumn = square.Column;
            int destRow = square.Row;

            if (destColumn == CurrColumn)
            {
                if (destRow > CurrRow) return IsMatch(square, 1, 0);
                if (destRow < CurrRow) return IsMatch(square, -1, 0);
            }
            else if (destRow == CurrRow)
            {
                if (destColumn > CurrColumn) return IsMatch(square, 0, 1);
                if (destColumn < CurrColumn) return IsMatch(square, 0, -1);
            }

            return false;
        }
    }
}