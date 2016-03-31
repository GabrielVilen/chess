using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Util;

namespace Chess.Pieces
{
    public abstract class Piece
    {
        protected int[][] Position { get; set; }
        protected bool IsClicked { get; set; }
        protected bool IsWhite { get;  }
        protected Enums.PieceType PieceType { get; set; }


        protected Piece(Enums.PieceType pieceType, bool isWhite)
        {
            PieceType = pieceType;
            IsWhite = isWhite;
        }

        public bool MoveTo(int[][] position)
        {
            // todo: check if position is free (valid)
            if (Board.Board.IsValid(this, position))
            {
                Position = position;
                return true;
            }
            return false;
        }
        public abstract void Click();
        public abstract int Kill();
      //  public abstract string ToString();
    }
}
