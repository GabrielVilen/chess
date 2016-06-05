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
    ///     Represents a bishop piece in the chess game.
    /// </summary>
    public class Bishop : Piece
    {
        /// <summary>
        ///     Creates a new bishop with the given color.
        /// </summary>
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.BishopBlack : Unicodes.BishopWhite);
        }

        public override bool CanMoveTo(Square square)
        {
            if (square.Row > CurrRow) return IsMatch(square, 1, 1)  || IsMatch(square, 1, -1);
            if (square.Row < CurrRow) return IsMatch(square, -1, 1) || IsMatch(square, -1, -1);

            return false;
        }
    }
}