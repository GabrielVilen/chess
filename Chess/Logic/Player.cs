using System.Collections.Generic;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Logic
{
    public class Player
    {
        public bool inCheck { get; private set; }
        public string Name { get; private set; }
        public Enums.Color Color { get; private set; }

        public int Score { get; set; }

        private List<Piece> myPieces = new List<Piece>();


        public Player(string name, Enums.Color color)
        {
            Name = name;
            Color = color;
        }

        public void AddPiece(Piece piece)
        {
            myPieces.Add(piece);
        }

        public void RemovePiece(Piece piece)
        {
            if (myPieces.Contains(piece))
                myPieces.Remove(piece);

            piece = null; // invoke GC
        }

        // todo test
        public bool CanCheck(Square destSquare)
        {
            Piece king = myPieces.Find(p => p.PieceType == Enums.PieceType.King);
            return king.CanMoveTo(destSquare);
        }

        internal bool TryMove(Square fromSquare, Square destSquare)
        {
            Piece piece = fromSquare.CurrPiece;

            if(myPieces.Contains(piece))
            {
                return piece.TryMoveTo(destSquare);
            }
            return false;
        }
    }
}