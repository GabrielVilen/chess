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
                return toRow > currRow ? LoopRows(currRow, toRow, toSquare) : LoopRows(0, currRow, toSquare);
            
            if (toRow == currRow)
                return toColumn > currColumn ? LoopColumns(currColumn, toColumn, toSquare) : LoopColumns(0, currColumn, toSquare);

            return false;
        }
        
        // todo unit test
        private bool LoopColumns(int start, int end, Square toSquare)
        {
            for (int i = start; i < end; i++)
            {
                Square square = board.GetSquare(currRow, i);
                if (square == null) break;
                if (square.IsSame(toSquare)) return true;
                if (!square.IsEmpty()) return false;
            }
            return false;
        }

        private bool LoopRows(int start, int end, Square toSquare)
        {
            for (int i = start; i < end; i++)
            {
                Square square = board.GetSquare(i, currColumn);
                if (square == null) break;
                if (square.IsSame(toSquare)) return true;
                if (!square.IsEmpty()) return false;
            }
            return false;
        }
    }
}
