using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    public class Square
    {
        public Enums.Color Color { get; }
        public Piece CurrPiece { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Square(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public Square(int row, int column, Enums.Color color)
        {
            Row = row;
            Column = column;
            Color = color;
        }
 
        public bool IsMovable(Piece piece)
        {
            if (CurrPiece == null) return true;
            return CurrPiece.Color != piece.Color || CurrPiece.PieceType == Enums.PieceType.None;
        }

        public Piece Move(Piece piece)
        {
            Piece oldPiece = CurrPiece;
            CurrPiece = piece;

            return oldPiece;
        }

        public bool IsEmpty()
        {
            if (CurrPiece == null) return true;
            return CurrPiece.PieceType == Enums.PieceType.None;
        }

        public bool IsSame(Square toSquare)
        {
            return (Row == toSquare.Row) && (Column == toSquare.Column);
        }
    }
}