using System;
using Chess.Util;
using Chess.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(Enums.Color color) : base(Enums.PieceType.King, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.King_black : Unicodes.King_white);
        }

        public override bool CanMoveTo(Square square)
        {
            return (Math.Abs(currRow - square.Row) < 2) && (Math.Abs(currColumn - square.Column) < 2)
                && !CanCheck(game.Opponent, square);
        }      


    }
}