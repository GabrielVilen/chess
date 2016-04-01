using Chess.Board;
using Chess.Util;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected Square Square { get; set; }
        protected bool IsClicked { get; set; }
        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }


        protected Piece(Enums.PieceType pieceType, Enums.Color color)
        {
            PieceType = pieceType;
            Color = color;
        }

        public void TryMoveTo(Square square)
        {
            if (IsLegalMove(square) && square.IsValid(this))
            {
                Piece destPiece = square.Move(this);

                if ((destPiece != null) && (destPiece.Color != Color))
                {
                    destPiece.Capture();
                    Board.Board.Score(destPiece, Color);
                }

                Square = square;
            }
        }

        internal abstract bool IsLegalMove(Square square);
        internal abstract void Click();
        internal abstract int Capture();
        //  public abstract string ToString();
    }
}