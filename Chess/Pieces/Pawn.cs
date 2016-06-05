using System;
using Chess.Util;
using Chess.Logic;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Pieces
{
    /// <summary>
    ///     Represents a pawn piece in the chess game. 
    /// </summary>
    public class Pawn : Piece
    {
        private bool isFirstMove = true;  // pawn can move two steps first move

        /// <summary>
        ///     Creates a new pawn with the given color.
        /// </summary>
        public Pawn(Enums.Color color) : base(Enums.PieceType.Pawn, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.PawnBlack : Unicodes.PawnWhite);
        }

        public override bool CanMoveTo(Square square)
        {
            // cannot move to non empty or diagonal squares unless capture
            if ((!square.IsEmpty() || Math.Abs(CurrColumn - square.Column) != 0) &&
                !CanCapture(square.CurrPiece))
                return false;

            // cannot move backwards        
            if (Color == Enums.Color.White && CurrRow <= square.Row ||
                Color == Enums.Color.Black && CurrRow >= square.Row)
                return false;
        
            if (isFirstMove)
            {
                // cannot move more than two steps
                if ((Math.Abs(CurrRow - square.Row) > 2) ||
                    !game.GetSquare(CurrRow + direction, CurrColumn).IsEmpty())
                    return false;

                isFirstMove = false;
            }
            // cannot move more than one step
            else if (Math.Abs(CurrRow - square.Row) > 1)
                return false;

            return true;
        }

        public override bool CanCapture(Piece destPiece)
        {      
            return destPiece != null && CanCapture(destPiece.CurrSquare);
        }

        public bool CanCapture(Square square)
        {
            int destRow = square.Row;
            int destColumn = square.Column;

            return (CurrRow + direction == destRow) && (CurrColumn + direction == destColumn || CurrColumn - direction == destColumn);
        }
    }
}