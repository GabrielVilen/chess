using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Util;

namespace Chess.Pieces
{
    class Pawn : Piece
    {
        private Pawn(bool isWhite) : base(Enums.PieceType.Pawn, isWhite)
        {
            throw new NotImplementedException();
        }

        public override void Click()
        {
            throw new NotImplementedException();
        }

        public override int Kill()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }

}
