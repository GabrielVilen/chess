using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected int next;
        protected bool IsClicked { get; set; }
        protected bool isFirstMove = true;

        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }

        protected int currColumn => CurrSquare.Column;
        protected int currRow => CurrSquare.Row;

        protected Square currSquare;

        public Square CurrSquare
        {
            get { return currSquare; }
            set { currSquare = value; }
        }

        protected Piece(Enums.PieceType pieceType, Enums.Color color)
        {
            PieceType = pieceType;
            Color = color;
            next = (color == Enums.Color.White ? -1 : 1); // next square is positive or negative 
        }

        // todo: prevent move if will be placed in check
        public void TryMoveTo(Square toSquare)
        {
            if (!IsSameSquare(toSquare) && IsLegalMove(toSquare) && toSquare.IsMovable(this))
            {
                Piece destPiece = toSquare.Move(this);

                if ((destPiece != null) && CanCapture(destPiece))
                {
                    destPiece.Capture();
                    Board.Score(destPiece, Color);

                    destPiece = null; // to invoke GC
                }
                isFirstMove = false;
                currSquare = toSquare;
            }
        }

        public abstract bool IsLegalMove(Square toSquare);

        /// <summary>
        ///     Should be overridden by pieces that has a different capturing pattern (eg. Pawn, that captures diagonal)
        /// </summary>
        public virtual bool CanCapture(Piece destPiece)
        {
            return true;
        }

        protected bool IsSameSquare(Square toSquare)
        {
            return currSquare == toSquare;
        }

        public void Click()
        {
            IsClicked = !IsClicked;
        }

        public Enums.PieceType Capture()
        {
            var currType = PieceType;
            PieceType = Enums.PieceType.None;

            return currType;
        }
    }
}