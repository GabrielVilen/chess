using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    internal abstract class Piece
    {
        protected int next;
        protected bool IsClicked { get; set; }
        protected bool isFirstMove = true;

        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }

        protected int currColumn => CurrSquare.Column;
        protected int currRow => CurrSquare.Row;

        protected Board board;
        protected Square currSquare;
        private Game.Game game;

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

            game = Game.Game.GetInstance();
            board = game.Board;
        }

        // todo: prevent move if will be placed in check
        public bool TryMoveTo(Square destSquare)
        {
            if (IsSameSquare(destSquare) || !CanMoveTo(destSquare) || !destSquare.CanPlace(this)) return false;

            Piece destPiece = destSquare.Move(this);

            if ((destPiece != null) && !IsSameColor(destPiece) && CanCapture(destPiece))
            {
                destPiece.Capture();
                game.Score(destPiece);
            }
            isFirstMove = false;
            currSquare = destSquare;

            return true;
        }

        /// <summary>
        ///     Loops the board by increasing by the given row and column offset.
        ///     Returns true if the piece can move to the destination square, and no pieces are blocking the path.
        ///     Can be overridden to replace with unique match pattern.
        /// </summary>
        protected virtual bool IsMatch(Square destSquare, int row, int column)
        {
            Square testSquare = currSquare;

            for (int i = 1; i < Board.TotalColumns; i++)
            {
                if (testSquare == null) continue;
                if (testSquare.IsSame(destSquare)) return true;
                if (!testSquare.IsEmpty()) return false;

                testSquare = board.GetSquare(testSquare.Row + row, testSquare.Column + column);
            }
            return false;
        }

        private bool IsSameColor(Piece destPiece)
        {
            return Color == destPiece.Color;
        }

        /// <summary>
        ///     Checks if the piece can move to the given destination square given the pice's movement pattern.
        ///     Override to implement movement pattern.
        /// </summary>
        public abstract bool CanMoveTo(Square destSquare);

        /// <summary>
        ///     Should only be overridden by pieces that has a different capturing pattern (eg. Pawn; captures diagonal)
        /// </summary>
        public virtual bool CanCapture(Piece destPiece)
        {
            return true;
        }

        protected bool IsSameSquare(Square destSquare)
        {
            return currSquare == destSquare;
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