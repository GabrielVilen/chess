using System;
using System.Collections.Generic;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    public class Player
    {
        public int Score { get; set; }
        public string Name { get; private set; }
        public Enums.Color Color { get; private set; }
        private List<Piece> pieces = new List<Piece>();
        public bool inCheck { get; set; } 

        public Player(string name, Enums.Color color)
        {
            Name = name;
            Color = color;
        }

        public void AddPiece(Piece piece)
        {
            pieces.Add(piece);
        }

        public void RemovePiece(Piece piece)
        {
            if (pieces.Contains(piece))
                pieces.Remove(piece);

            piece = null; // invoke GC
        }

 
        // todo test
        public bool CanCheck(Square destSquare)
        {
            Piece king = pieces.Find(p => p.PieceType == Enums.PieceType.King);
            return king.CanMoveTo(destSquare);
        }
    }
}