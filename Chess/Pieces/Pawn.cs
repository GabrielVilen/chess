using System;
using Chess.Gui;
using Chess.Util;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private bool isFirstMove = true;

        public Pawn(Enums.Color color) : base(Enums.PieceType.Pawn, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.Pawn_black : Unicodes.Pawn_white);
        }

        public override bool CanMoveTo(Square destSquare)
        {
            if (Color == Enums.Color.White && currRow <= destSquare.Row ||
                Color == Enums.Color.Black && currRow >= destSquare.Row) // checks direction 
                return false;

            if (Math.Abs(currColumn - destSquare.Column) != 0) return false;
            if (isFirstMove)
            {
                if ((Math.Abs(currRow - destSquare.Row) > 2) || 
                    !Game.GetInstance().GetSquare(currRow + next, currColumn).IsEmpty())
                    return false;
            }
            else if (Math.Abs(currRow - destSquare.Row) > 1) return false;

            isFirstMove = false;

            return true;
        }

        public override bool CanCapture(Piece destPiece)
        {
            if (destPiece.CurrSquare == null) return false;
            int destRow = destPiece.CurrSquare.Row;
            int destColumn = destPiece.CurrSquare.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }
    }
}