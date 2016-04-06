using System.Diagnostics;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
        }
        
        public override bool CanMoveTo(Square toSquare)
        {
            if (toSquare.Color != Color) return false;
            if (LoopTest(toSquare, 1, 1)) return true;
            if (LoopTest(toSquare, 1, -1)) return true;
            if (LoopTest(toSquare, -1, 1)) return true;
            if (LoopTest(toSquare, -1, -1)) return true;

            return false;
        }

        private bool LoopTest(Square toSquare, int row, int column)
        {
            Square square = currSquare;

            for (int i = 1; i < Board.TotalColumns; i++)
            {
                if (square == null) break;          
                if (square.IsSame(toSquare)) return true;
                if (!square.IsEmpty()) return false;
                
                square = board.GetSquare(square.Row + row, square.Column + column);
            }
            return false;
        }
    }
}