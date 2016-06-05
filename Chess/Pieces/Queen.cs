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
    ///     Represents a queen piece in the chess game.
    /// </summary>
    public class Queen : Piece
    {
        /// <summary>
        ///     Creates a new queen with the given color.
        /// </summary>
        public Queen(Enums.Color color) : base(Enums.PieceType.Queen, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.QueenBlack : Unicodes.QueenWhite);
        }

        public override bool CanMoveTo(Square square)
        {
            int destColumn = square.Column;
            int destRow = square.Row;

            // loop columns 
            if (destColumn == CurrColumn)
            {
                if (destRow > CurrRow) return IsMatch(square,  1, 0);
                if (destRow < CurrRow) return IsMatch(square, -1, 0);
            }
            // loop rows
            else if (destRow == CurrRow)
            {
                if (destColumn > CurrColumn) return IsMatch(square, 0,  1);
                if (destColumn < CurrColumn) return IsMatch(square, 0, -1);
            }

            // loop diagonal
            if (destRow > CurrRow) return IsMatch(square,  1, 1) || IsMatch(square,  1, -1);
            if (destRow < CurrRow) return IsMatch(square, -1, 1) || IsMatch(square, -1, -1);

            return false;
        }
    }
}