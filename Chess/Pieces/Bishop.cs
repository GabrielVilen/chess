using System.Diagnostics;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    // todo Bishop
    public class Bishop : Piece
    {
        public Bishop(Enums.Color color) : base(Enums.PieceType.Bishop, color)
        {
        }

        // todo unit test if piece in between
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
            Square testSquare = board.GetSquare(currRow, currColumn);

            for (int i = 1; i < Board.Columns; i++)
            {
                if (testSquare == null) break;          // todo why null ?
                if (testSquare.IsSame(toSquare)) return true;
                if (!testSquare.IsEmpty()) return false;
                
                testSquare = board.GetSquare(testSquare.Row + row, testSquare.Column + column);
            }
            return false;
        }
    }
}