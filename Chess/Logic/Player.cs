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

        private List<Piece> pieces = new List<Piece>();
        public List<Piece> Pieces { get { return pieces; } }

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

        internal bool TryMove(Square fromSquare, Square clickedSquare)
        {
            Piece piece = fromSquare.CurrPiece;

            if (pieces.Contains(piece))
            {
                return piece.TryMoveTo(clickedSquare);
            }
            return false;
        }
    }
}