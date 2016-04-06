using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class Knight : Piece
    {
        public Knight(Enums.Color color) : base(Enums.PieceType.Knight, color)
        {
        }
        
        public override bool CanMoveTo(Square toSquare)
        {
            return TestMoves(2, 1, toSquare) || TestMoves(1, 2, toSquare);
        }

        private bool TestMoves(int row, int col, Square toSquare)
        {
            int toRow = toSquare.Row;
            int toCol = toSquare.Column;

            if ((toRow == currRow + row) && (toCol == currColumn + col)) return true;
            if ((toRow == currRow + row) && (toCol == currColumn - col)) return true;
            if ((toRow == currRow - row) && (toCol == currColumn + col)) return true;
            if ((toRow == currRow - row) && (toCol == currColumn - col)) return true;

            return false;
        }
    }
}