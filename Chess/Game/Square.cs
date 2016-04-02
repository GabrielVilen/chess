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
        

        public Square(int row, int column, Enums.Color color)
        {
            Row = row;
            Column = column;
            Color = color;
        }

        public bool IsValid(Piece piece)
        {
            return CurrPiece == null || CurrPiece.Color != piece.Color;
        }

        public Piece Move(Piece piece)
        {
            Piece oldPiece = CurrPiece;
            CurrPiece = piece;

            return oldPiece;
        }

        public bool IsEmpty()
        {
            return CurrPiece == null || CurrPiece.PieceType == Enums.PieceType.None;
        }
    }
}
