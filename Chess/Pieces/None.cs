using Chess.Logic;
using Chess.Pieces;

namespace Chess
{
    internal class None : Piece
    {
        public None() : base(Util.Enums.PieceType.None, Util.Enums.Color.Black)
        {
            Unicode = "";
        }

        public override bool CanMoveTo(Square clickedSquare)
        {
            return false;
        }
    }
}