using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    // todo Knight
    public class Knight : Piece
    {
        public Knight(Enums.Color color) : base(Enums.PieceType.Knight, color)
        {
        }

        // todo: unit test
        public override bool CanMoveTo(Square toSquare)
        {
            return TestSquares(2, 1, toSquare) || TestSquares(1, 2, toSquare);
        }

        private bool TestSquares(int row, int col, Square toSquare)
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