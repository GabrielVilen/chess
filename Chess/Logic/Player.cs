using System.Collections.Generic;
using Chess.Pieces;
using Chess.Util;
using System.Diagnostics;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
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

        private readonly List<Piece> pieces = new List<Piece>();

        public List<Piece> Pieces => pieces;

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

        /// <summary>
        ///     Tries to remove the given piece from this player's piece list, if it exists in the list. 
        ///     Returns true if the piece has been removed.
        /// </summary>
        public bool RemovePiece(Piece piece)
        {
            return pieces.Remove(piece);
        }

        internal bool TryMove(Square fromSquare, Square square)
        {
            Piece piece = fromSquare.CurrPiece;

            return pieces.Contains(piece) && piece.TryMoveTo(square);
        }
    }
}