using System;
using Chess.Util;
using Chess.Logic;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private bool isFirstMove = true;

        public Pawn(Enums.Color color) : base(Enums.PieceType.Pawn, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Pawn_black : Unicodes.Pawn_white);
        }

        public override bool CanMoveTo(Square square)
        {
            // cannot move to non empty or diagonal squares unless capture
            if ((!square.IsEmpty() || Math.Abs(currColumn - square.Column) != 0) &&
                !CanCapture(square.CurrPiece))
                return false;

            // cannot move backwards        
            if (Color == Enums.Color.White && currRow <= square.Row ||
                Color == Enums.Color.Black && currRow >= square.Row)
                return false;
        
            if (isFirstMove)
            {
                // cannot move more than two steps
                if ((Math.Abs(currRow - square.Row) > 2) ||
                    !game.GetSquare(currRow + next, currColumn).IsEmpty())
                    return false;

                isFirstMove = false;
            }
            // cannot move more than one step
            else if (Math.Abs(currRow - square.Row) > 1)
                return false;

            return true;
        }

        public override bool CanCapture(Piece destPiece)
        {      
            return destPiece == null ? false : CanCapture(destPiece.CurrSquare);
        }

        public bool CanCapture(Square square)
        {
            int destRow = square.Row;
            int destColumn = square.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }
    }
}