using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    internal class Player
    {
        public int Score { get; set; }
        public string Name { get; private set; }
        public Enums.Color Color { get; private set; }
        private List<Piece> pieces = new List<Piece>();
        private bool isCheck;

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

            piece = null;
        }

        public bool InCheck()
        {
            return isCheck;
        }

        // todo WillBeInCheck
        public bool WillBeInCheck(Square destSquare)
        {
            //pieces.Find()
            throw new NotImplementedException();
        }
    }
}
