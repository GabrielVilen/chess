using System.Collections.Generic;
using Chess.Pieces;
using Chess.Util;
using System.Diagnostics;

namespace Chess.Logic
{
    public class Player
    {
        public string Name { get; private set; }
        public Enums.Color Color { get; private set; }
        private bool inCheck;
        public bool InCheck
        {
            get { return inCheck; }
            set
            {
                inCheck = value;
                Debug.WriteLineIf(inCheck, "In check!", Color.ToString());
            }
        }
        public int Score { get; set; }

        private List<Piece> pieces = new List<Piece>();

        public List<Piece> Pieces { get { return pieces; } }
        public King King
        {
            get
            {
                return (King)pieces.Find(piece => piece is King);
            }
        }

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

        internal bool TryMove(Square fromSquare, Square square)
        {
            Piece piece = fromSquare.CurrPiece;

            if (pieces.Contains(piece))
            {
                return piece.TryMoveTo(square);
            }
            return false;
        }
    }
}