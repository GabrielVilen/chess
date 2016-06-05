using System;
using Chess.Util;
using Chess.Logic;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Pieces
{
    /// <summary>
    ///     Represents a king piece in the chess game.
    /// </summary>
    public class King : Piece
    {
        /// <summary>
        ///     Creates a new king with the given color.
        /// </summary>
        public King(Enums.Color color) : base(Enums.PieceType.King, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.KingBlack : Unicodes.KingWhite);
        }

        public override bool CanMoveTo(Square square)
        {
            return (Math.Abs(CurrRow - square.Row) < 2) && (Math.Abs(CurrColumn - square.Column) < 2)
                && !CanCheck(game.Opponent, square);
        }      


    }
}