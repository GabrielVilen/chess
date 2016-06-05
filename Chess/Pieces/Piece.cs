using Chess.Logic;
using Chess.Util;
using System.Collections.Generic;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Pieces
{
    /// <summary>
    ///     Superclass which represents a single piece in the game. Contains data and operations on the piece.
    /// </summary>
    public abstract class Piece
    {
        protected int direction;
        public bool IsClicked { get; set; }
        public string Unicode { get; protected set; }

        public Enums.Color Color { get; set; }
        public Enums.PieceType PieceType { get; set; }

        protected int CurrColumn => currSquare.Column;
        protected int CurrRow => currSquare.Row;

        protected Square currSquare;
        protected Game game;

        private Player CurrPlayer => game.CurrPlayer;
        private Player Opponent => game.Opponent;

        public Square CurrSquare
        {
            get { return currSquare; }
            set { currSquare = value; }
        }

        /// <summary>
        ///     Creates a new piece of the given type with the given color.
        /// </summary>
        /// <param name="pieceType"></param>
        /// <param name="color"></param>
        protected Piece(Enums.PieceType pieceType, Enums.Color color)
        {
            PieceType = pieceType;
            Color = color;
            direction = (color == Enums.Color.White ? -1 : 1); // sets direction to up or down depening on piece color

            game = Game.GetInstance();
        }

        // todo: implement shackmatt!
        // todo: transform pawn to queen
        // todo: implement castling (sv. rockad)
        /// <summary>
        ///     Tries to move this piece to the given square by calling various method to check:  <para></para>
        ///     - Piece can be moved to the square                                                <para></para>
        ///     - Current player is not in check                                                  <para></para>
        ///     - The move does not place the player in check.       <para></para>
        ///     Returns true if the piece can be moved to the square.
        /// </summary>
        public bool TryMoveTo(Square square)
        {
            if (!CanBeMovedTo(square))
                return false;

            Piece newPiece = square.Move(this);

            if (IsValidPiece(newPiece))
            {
                newPiece.Capture();
                game.Score(newPiece);
            }
            currSquare = square;

            Opponent.InCheck = CanCheck(this, Opponent.King);
            return true;
        }

        /// <summary>
        ///     Tries to move this piece to the given square. 
        ///     Returns true if the current player is in check after the move (opponent can check the king).
        /// </summary>
        private bool InCheckAfterMove(Square square)
        {
            // saves current state
            Square saveSquare = currSquare;
            Piece oldPiece = square.Move(this);
            Piece removedPiece = currSquare.RemovePiece();
            currSquare = square;

            bool hasRemoved = Opponent.RemovePiece(oldPiece);
            bool inCheck = CanCheck(Opponent, CurrPlayer.King.CurrSquare);

            // restore state
            square.Move(oldPiece);
            currSquare = saveSquare;
            currSquare.SetPiece(removedPiece);

            if (hasRemoved) Opponent.AddPiece(oldPiece);

            return inCheck;
        }

        /// <summary>
        ///     Returns true if the piece is "valid"; it exists, does not have the same color as this piece, 
        ///     and can be captured by this piece.
        /// </summary>
        private bool IsValidPiece(Piece newPiece)
        {
            return newPiece != null
                && newPiece.PieceType != Enums.PieceType.None
                && !IsSameColor(newPiece)
                && CanCapture(newPiece);
        }

        /// <summary>
        ///     Returns true if this piece can move to the given square and the player will not be placed in check 
        ///     after the move. 
        /// </summary>
        private bool CanBeMovedTo(Square square)
        {
            return !InCheckAfterMove(square) && CanMoveTo(square) && square.CanPlace(this);
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
                if (testSquare != null)
                {
                    testSquare = game.GetSquare(testSquare.Row + row, testSquare.Column + column);

                    if (testSquare == null) continue;
                    if (testSquare.IsSame(square)) return true;
                    if (!testSquare.IsEmpty() && !testSquare.IsSame(currSquare)) return false;
                }
            }
            return false;
        }

        /// <summary>
        ///     Returns true if the given piece is the same color as this piece.
        /// </summary>
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
        ///     Returns true if the given player can check the given square
        /// </summary>
        internal bool CanCheck(Player player, Square square)
        {
            return CanCheck(player.Pieces, square);
        }

        /// <summary>
        ///     Returns true if the given piece can check the given king
        /// </summary>
        internal bool CanCheck(Piece piece, King king)
        {
            return CanCheck(new List<Piece> { piece }, king.CurrSquare);
        }

        /// <summary>
        ///     Loops the given pieces and returns true if any of the pieces can check the given square.
        /// </summary>
        private bool CanCheck(IEnumerable<Piece> pieces, Square square)
        {
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

        /// <summary>
        ///     Represents a GUI click on this piece. Returns true if the piece is clicked after the click.
        /// </summary>
        public bool Click()
        {
            return IsClicked = !IsClicked;
        }

        /// <summary>
        ///     Captures this piece; sets the current piece type to none and returns the current type.
        /// </summary>
        public Enums.PieceType Capture()
        {
            var currType = PieceType;
            PieceType = Enums.PieceType.None;

            return currType;
        }

        /// <summary>
        ///     Returns the chess unicode representing the piece. 
        /// </summary>
        public override string ToString()
        {
            return Unicode;
        }

    }
}