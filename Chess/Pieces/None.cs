using System;
using Chess.Gui;
using Chess.Pieces;

namespace Chess
{
    internal class None : Piece
    {
        public None() : base(Util.Enums.PieceType.None, Util.Enums.Color.Black)
        {
        }

        public override bool CanMoveTo(Square destSquare)
        {
            return false;
        }
    }
}