using Chess.Logic;
using Chess.Util;
using System.Diagnostics;
using System;
using System.Collections.Generic;

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
        protected Game game;

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
        // todo: gui bugg? shackade förblir grå
        public bool TryMoveTo(Square square)
        {
            if (!CanBeMovedTo(square) || CanCheckRemoved(game.Opponent, currSquare, game.CurrPlayer.King))
                return false;

            Piece newPiece = square.Move(this);

            if (IsValidPiece(newPiece))
            {
                newPiece.Capture();
                game.Score(newPiece);
            }
            currSquare = square;

            game.Opponent.InCheck = CanCheck(this, game.Opponent.King);  // kolla över om man ska ta bort incheck?
            return true;
        }

        /// <summary>
        ///      Temporarily remove the current piece at the square.
        ///      Returns true if if the player can capture the king after the piece has been removed.
        /// </summary>
        private bool CanCheckRemoved(Player player, Square square, King king)
        {
            Piece piece = square.RemovePiece();
            bool isCheck = CanCheck(player, king.currSquare);

            if (piece != null)
                square.SetPiece(piece);

           // game.SetOpponentInCheck(player, isCheck);

            return isCheck;
        }

        private bool IsValidPiece(Piece newPiece)
        {
            return newPiece != null && newPiece.PieceType != Enums.PieceType.None && !IsSameColor(newPiece) && CanCapture(newPiece);
        }

        private bool CanBeMovedTo(Square square)
        {
            return !IsInCheck(game.CurrPlayer) && CanMoveTo(square) && square.CanPlace(this);
        }

        /// <summary>
        ///     Loops the table by increasing by the given row and column offset.
        ///     Returns true if the piece can move to the destination square, and no pieces are blocking the path.
        ///     Can be overridden to replace with unique match pattern.
        /// </summary>
        protected virtual bool IsMatch(Square square, int row, int column)
        {
            Square testSquare = currSquare;

            for (int i = 1; i < Game.MaxColumn; i++)
            {
                testSquare = game.GetSquare(testSquare.Row + row, testSquare.Column + column);

                if (testSquare == null) continue;
                if (testSquare.IsSame(square)) return true;
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
        public abstract bool CanMoveTo(Square square);

        /// <summary>
        ///     Should only be overridden by pieces that has a different capturing pattern (eg. Pawn; captures diagonal)
        /// </summary>
        public virtual bool CanCapture(Piece destPiece)
        {
            return true;
        }

        /// <summary>
        /// Returns true if the given player can check the given square
        /// </summary>
        internal bool CanCheck(Player player, Square square)
        {
            return CanCheck(player.Pieces, square); // removed king reqruiment
        }

        /// <summary>
        /// Returns true if the given piece can check the given king
        /// </summary>
        internal bool CanCheck(Piece piece, King king)
        {
            var p = new List<Piece>();
            p.Add(piece);

            return CanCheck(p, king.CurrSquare);
        }

        // todo flytta CanCheck till game klassne?
        private bool CanCheck(List<Piece> pieces, Square square)
        {
            //Debug.WriteLine("CanCheck(list,{1})", pieces, square);

            foreach (Piece piece in pieces)
            {
                if (piece is Pawn)
                {
                    if (((Pawn)piece).CanCapture(square))
                    {
                        return true;
                    }
                }
                else if (piece.PieceType != Enums.PieceType.King && piece.CanMoveTo(square))
                {
                    return true;
                }
            }
            return false;
        }

        // todo testa
        private bool IsInCheck(Player player)
        {
            return player.InCheck && !(this is King); //  && currSquare != square
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