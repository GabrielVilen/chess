using Chess.Logic;
using Chess.Util;
using System.Diagnostics;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected int next;
        public bool IsClicked { get; set; }
        public string Unicode { get; protected set; }

        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }

        protected int currColumn => CurrSquare.Column;
        protected int currRow => CurrSquare.Row;

        protected Square currSquare;
        private Game game;

        public Square CurrSquare
        {
            get { return currSquare; }
            set { currSquare = value; }
        }

        protected Piece(Enums.PieceType pieceType, Enums.Color color)
        {
            PieceType = pieceType;
            Color = color;
            next = (color == Enums.Color.White ? -1 : 1); // sets next square to positive or negative 

            game = Game.GetInstance();
        }

        // todo: transform pawn to queen
        // todo: implement castling (sv. rockad)
        public bool TryMoveTo(Square destSquare)
        {
            if (!IsValidSquare(destSquare) || !CanMoveTo(destSquare) || !destSquare.CanPlace(this))
                return false;

            Piece newPiece = destSquare.Move(this);

            if (newPiece != null && newPiece.PieceType != Enums.PieceType.None && !IsSameColor(newPiece) && CanCapture(newPiece))
            {
                newPiece.Capture();
                game.Score(newPiece);
            }

            Debug.WriteLine("moved {0} @ [{1},{2}] to {3} @ [{4},{5}] hash: {6}", this, currSquare.Row, currSquare.Column, newPiece, destSquare.Row, destSquare.Column, destSquare.GetHashCode());

            currSquare = destSquare;

            return true;
        }

        /// <summary>
        ///     Loops the table by increasing by the given row and column offset.
        ///     Returns true if the piece can move to the destination square, and no pieces are blocking the path.
        ///     Can be overridden to replace with unique match pattern.
        /// </summary>
        protected virtual bool IsMatch(Square destSquare, int row, int column)
        {
            Square testSquare = currSquare;

            for (int i = 1; i < Game.MaxColumn; i++)
            {
                if (testSquare == null) continue;
                if (testSquare.IsSame(destSquare)) return true;
                if (!testSquare.IsEmpty()) return false;

                testSquare = game.GetSquare(testSquare.Row + row, testSquare.Column + column);
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

        protected bool IsValidSquare(Square destSquare)
        {
            if (game.InCheck(Color, destSquare)) return false;
            return currSquare != destSquare;
        }

        public bool Click()
        {
            return IsClicked = !IsClicked;
        }

        public Enums.PieceType Capture()
        {
            var currType = PieceType;
            PieceType = Enums.PieceType.None;

            return currType;
        }

        public override string ToString()
        {
            return Unicode;
        }

    }
}