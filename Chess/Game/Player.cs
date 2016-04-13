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


        public Player(string name, Enums.Color color)
        {
            Name = name;
            Color = color;
        }

        public void RemovePiece(Piece destPiece)
        {
            // todo  implement RemovePiece
            throw new NotImplementedException();
        }
    }
}
