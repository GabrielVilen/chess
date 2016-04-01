using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Board
{
    public class Square
    {
        public Enums.Color Color { get; }
        public Piece CurrPiece { get; set; }

        public Square(Piece currPiece, Enums.Color color)
        {
            Color = color;
            CurrPiece = currPiece;
        }

        public bool IsValid(Piece piece)
        {
            return CurrPiece == null || CurrPiece.Color != piece.Color;
        }

        public Piece Move(Piece piece)
        {
            Piece oldPiece = CurrPiece;
            CurrPiece = piece;

            return oldPiece;
        }
    }
}
