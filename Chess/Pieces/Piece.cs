using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected int next;
        protected bool IsClicked { get; set; }
        protected Square currSquare;

        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }

        public Square CurrSquare
        {
            get { return currSquare; }
            set { currSquare = value; }
        }

        protected Piece(Enums.PieceType pieceType, Enums.Color color)
        {
            PieceType = pieceType;
            Color = color;
            next = (color == Enums.Color.White ? -1 : 1); // increment or decrement if black or white
        }

        public void TryMoveTo(Square toSquare)
        {
            if (IsLegalMove(toSquare) && toSquare.IsValid(this))
            {
                Piece destPiece = toSquare.Move(this);

                if ((destPiece != null) && CanCapture(destPiece))
                {
                    destPiece.Capture();
                    Board.Score(destPiece, Color);

                    destPiece = null; // to invoke GC
                }
                currSquare = toSquare;
            }
        }

        public abstract bool CanCapture(Piece destPiece);

        public abstract bool IsLegalMove(Square toSquare);


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