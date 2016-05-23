using Chess.Pieces;
using Chess.Util;

namespace Chess.Gui
{
    public class Square
    {
        public int Row { get; }
        public int Column { get; }
        public Enums.Color Color { get; }
        public Piece CurrPiece { get; private set; }
        public string CurrUnicode { get; private set; }

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

            SetPiece(new None()); // todo look over
        }

        public bool CanPlace(Piece piece)
        {
            if (CurrPiece == null) return true;
            return CurrPiece.Color != piece.Color || CurrPiece.PieceType == Enums.PieceType.None;
        }

        public void SetPiece(Piece piece)
        {
            CurrPiece = piece;
            CurrUnicode = piece.Unicode;
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

        public bool IsSame(Square destSquare)
        {
            return (Row == destSquare.Row) && (Column == destSquare.Column);
        }

        public override string ToString()
        {
            return CurrPiece != null ? CurrPiece.Unicode : "X"; // todo not working with data binding
        }
    }
}