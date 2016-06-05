using Chess.Pieces;
using Chess.Util;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Logic
{
    /// <summary>
    ///     Represents a single square in the chess game and contains information about the square. 
    /// </summary>
    public class Square
    {
        public int Row { get; }
        public int Column { get; }
        public Enums.Color Color { get; }
        public Piece CurrPiece { get; private set; }

        public string CurrUnicode { get; set; }

        /// <summary>
        ///     Creates a new square with the given row and column number, and color.
        /// </summary>
        public Square(int row, int column, Enums.Color color)
        {
            Row = row;
            Column = column;
            Color = color;
        }

        /// <summary>
        ///     Retruns true if the piece can be placed at this square; 
        ///     it is empty or the current piece is of another color (it can be captured by the piece).
        /// </summary>
        public bool CanPlace(Piece piece)
        {
            return IsEmpty() || CurrPiece.Color != piece.Color;
        }

        /// <summary>
        ///     Sets the given piece as the current piece at this square.
        /// </summary>
        public void SetPiece(Piece piece)
        {
            CurrPiece = piece;
            CurrUnicode = piece.Unicode;
        }

        /// <summary>
        ///     Moves the given piece to this square. Returns the old piece at the square.
        /// </summary>
        public Piece Move(Piece piece)
        {
            Piece oldPiece = CurrPiece;
            CurrPiece = piece;

            return oldPiece;
        }

        /// <summary>
        ///     Returns true if there is no piece at this square.
        /// </summary>
        public bool IsEmpty()
        {
            return CurrPiece == null || CurrPiece.PieceType == Enums.PieceType.None;
        }

        /// <summary>
        ///     Retunrs true if the given square has the same row and column as this square. 
        /// </summary>
        public bool IsSame(Square square)
        {
            return (Row == square.Row) && (Column == square.Column);
        }

        /// <summary>
        ///     Retruns the unicode of the current piece (if there is one).
        /// </summary>
        public override string ToString()
        {
            return CurrPiece != null ? CurrPiece.Unicode : "";
        }

        /// <summary>
        ///     Removes the piece (if there is one) currently at the square. Returns the removed piece.
        /// </summary>
        internal Piece RemovePiece()
        {
            Piece oldPiece = CurrPiece;
            if (CurrPiece != null)
            {
                CurrPiece = null;
                CurrUnicode = "";
            }
            return oldPiece;
        }

        /// <summary>
        ///     Retruns true if the current piece at this square is clicked.
        /// </summary>
        internal bool IsClicked()
        {
            return CurrPiece != null && CurrPiece.IsClicked;
        }

        /// <summary>
        ///     Clicks the current piece (if there is one) at this square.
        /// </summary>
        internal bool Click()
        {
            return CurrPiece != null && CurrPiece.Click();
        }

    }
}