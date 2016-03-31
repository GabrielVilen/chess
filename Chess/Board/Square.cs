using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Util;

namespace Chess.Board
{
    public class Square
    {
        public Enums.Color Color { get; }
        public Enums.PieceType CurrPiece { get; set; }

        public Square(Enums.PieceType currPiece, Enums.Color color)
        {
            Color = color;
            CurrPiece = currPiece;
        }

    }
}
