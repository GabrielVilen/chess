using Chess.Logic;
using Chess.Util;
using System.Diagnostics;
using System;

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
        public bool TryMoveTo(Square clickedSquare)
        {
            if (!CanBeMovedTo(clickedSquare)) return false;

            Piece newPiece = clickedSquare.Move(this);

            if (IsValidPiece(newPiece))
            {
                newPiece.Capture();
                game.Score(newPiece);
            }
            currSquare = clickedSquare;

            return true;
        }

        private bool IsValidPiece(Piece newPiece)
        {
            return newPiece != null && newPiece.PieceType != Enums.PieceType.None && !IsSameColor(newPiece) && CanCapture(newPiece);
        }

        private bool CanBeMovedTo(Square clickedSquare)
        {
            return IsValidSquare(clickedSquare) && CanMoveTo(clickedSquare) && clickedSquare.CanPlace(this);
        }

        /// <summary>
        ///     Loops the table by increasing by the given row and column offset.
        ///     Returns true if the piece can move to the destination square, and no pieces are blocking the path.
        ///     Can be overridden to replace with unique match pattern.
        /// </summary>
        protected virtual bool IsMatch(Square clickedSquare, int row, int column)
        {
            Square testSquare = currSquare;

            for (int i = 1; i < Game.MaxColumn; i++)
            {
                testSquare = game.GetSquare(testSquare.Row + row, testSquare.Column + column);

                if (testSquare == null) continue;
                if (testSquare.IsSame(clickedSquare)) return true;
                if (!testSquare.IsEmpty() && !testSquare.IsSame(currSquare)) return false;
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
        public abstract bool CanMoveTo(Square clickedSquare);

        /// <summary>
        ///     Should only be overridden by pieces that has a different capturing pattern (eg. Pawn; captures diagonal)
        /// </summary>
        public virtual bool CanCapture(Piece destPiece)
        {
            return true;
        }

        // todo test
        private bool IsValidSquare(Square clickedSquare)
        {
            return !game.CurrPlayer.inCheck && currSquare != clickedSquare;
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