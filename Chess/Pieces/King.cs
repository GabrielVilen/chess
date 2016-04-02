using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(Enums.Color color) : base(Enums.PieceType.King, color)
        {
        }

        public override bool CanMoveTo(Square toSquare)
        {
            return (Math.Abs(currRow - toSquare.Row) < 2) && (Math.Abs(currColumn - toSquare.Column) < 2);
        }
    }
}
