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

        public override bool CanMoveTo(Square toSquare)
        {
            int toColumn = toSquare.Column;
            int toRow = toSquare.Row;

            if (toColumn == currColumn)
            {
                if (toRow > currRow) return Loop(currRow, toRow, toSquare, true);
                if (toRow < currRow) return Loop(1, currRow, toSquare, true);
            }
            else if (toRow == currRow)
            {
                if (toColumn > currColumn) return Loop(currColumn, toColumn, toSquare, false);
                if (toColumn < currColumn) return Loop(1, currColumn, toSquare, false);
            }

            return false;
        }

        private bool Loop(int start, int end, Square toSquare, bool loopRows)
        {
            for (int i = start; i <= end; i++)
            {
                Square square = loopRows ? board.GetSquare(i, currColumn) : board.GetSquare(currRow, i);
                if (square == null) continue;
                if (!square.IsEmpty()) return false;
                if (square.IsSame(toSquare)) return true;
            }
            return false;
        }
    }
}