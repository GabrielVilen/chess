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

        public override bool CanMoveTo(Square clickedSquare)
        {
            // cannot move to non empty or diagonal squares unless capture
            if ((!clickedSquare.IsEmpty() || Math.Abs(currColumn - clickedSquare.Column) != 0) &&
                !CanCapture(clickedSquare.CurrPiece))
                return false;

            // cannot move backwards        
            if (Color == Enums.Color.White && currRow <= clickedSquare.Row ||
                Color == Enums.Color.Black && currRow >= clickedSquare.Row)
                return false;
        
            if (isFirstMove)
            {
                // cannot move more than two steps
                if ((Math.Abs(currRow - clickedSquare.Row) > 2) ||
                    !Game.GetInstance().GetSquare(currRow + next, currColumn).IsEmpty())
                    return false;

                isFirstMove = false;
            }
            // cannot move more than one step
            else if (Math.Abs(currRow - clickedSquare.Row) > 1)
                return false;

            return true;
        }

        public override bool CanCapture(Piece destPiece)
        {
            if (destPiece == null) return false;
            int destRow = destPiece.CurrSquare.Row;
            int destColumn = destPiece.CurrSquare.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }

        public bool CanCapture(Square clickedSquare)
        {
            int destRow = clickedSquare.Row;
            int destColumn = clickedSquare.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }
    }
}