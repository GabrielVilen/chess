using System;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        private bool isFirstMove;

        public Pawn(Enums.Color color) : base(Enums.PieceType.Pawn, color)
        {
            isFirstMove = true;
        }

        // todo: check capture
        public override bool CanCapture(Piece destPiece)
        {
            if ((destPiece.PieceType == Enums.PieceType.None) || (destPiece.Color == Color)) return false;

            int destRow = destPiece.CurrSquare.Row;
            int destColumn = destPiece.CurrSquare.Column;
            int currRow = CurrSquare.Row;
            int currColumn = CurrSquare.Column;

            return (currRow + next == destRow) && (currColumn + next == destColumn || currColumn - next == destColumn);
        }

        public override bool IsLegalMove(Square toSquare)
        {
            if (currSquare == toSquare) return false;
            if (isFirstMove)
            {
                if ((Math.Abs(currSquare.Row - toSquare.Row) > 2) ||
                    !Board.GetInstance().GetSquareByRow(currSquare.Row + next).IsEmpty())
                    return false;
            }
            else if (Math.Abs(currSquare.Row - toSquare.Row) > 1) return false;

            return true;
        }
    }
}