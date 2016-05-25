using System;
using Chess.Util;
using Chess.Logic;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(Enums.Color color) : base(Enums.PieceType.King, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.King_black : Unicodes.King_white);
        }

        public override bool CanMoveTo(Square destSquare)
        {
            return (Math.Abs(currRow - destSquare.Row) < 2) && (Math.Abs(currColumn - destSquare.Column) < 2);
        }
    }
}