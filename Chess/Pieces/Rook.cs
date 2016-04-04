using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    // todo Rook
    class Rook : Piece
    {
        public Rook(Enums.Color color) : base(Enums.PieceType.Rook, color)
        {
        }

        public override bool CanMoveTo(Square toSquare)
        {
            if (toSquare.Row != currRow || toSquare.Column != currColumn) return false;

            if (toSquare.Column > currColumn)
            {
                if(LoopColumns(currColumn, toSquare.Column)) return true;
            }
            return false;
        }

        // todo complete
        private bool LoopColumns(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (board.Squares[currRow, i] == currSquare) return true; 
            }
            return false;
        }
    }
}
