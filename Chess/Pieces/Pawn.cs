using System;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Enums.Color color) : base(Enums.PieceType.Pawn, color)
        {
        }

        public override bool CanMoveTo(Square destSquare)
        {
            if (Color == Enums.Color.White && currRow <= destSquare.Row ||
                Color == Enums.Color.Black && currRow >= destSquare.Row) // check direction 
                return false;

            if (Math.Abs(currColumn - destSquare.Column) != 0) return false;

            if (isFirstMove)
            {
                if ((Math.Abs(currRow - destSquare.Row) > 2) || !board.GetSquare(currRow + next, currColumn).IsEmpty())
                    return false;
            }
            else if (Math.Abs(currRow - destSquare.Row) > 1) return false;

            return true;
        }

        public override bool CanCapture(Piece destPiece)
        {
            int destRow = destPiece.CurrSquare.Row;
            int destColumn = destPiece.CurrSquare.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }
    }
}