using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class Rook : Piece
    {
        public Rook(Enums.Color color) : base(Enums.PieceType.Rook, color)
        {
        }

        public override bool CanMoveTo(Square destSquare)
        {
            int destColumn = destSquare.Column;
            int destRow = destSquare.Row;

            if (destColumn == currColumn)
            {
                if (destRow > currRow) return IsMatch(destSquare, 1, 0);
                if (destRow < currRow) return IsMatch(destSquare, -1, 0);
            }
            else if (destRow == currRow)
            {
                if (destColumn > currColumn) return IsMatch(destSquare, 0, 1);
                if (destColumn < currColumn) return IsMatch(destSquare, 0, -1);
            }

            return false;
        }
        
    }
}