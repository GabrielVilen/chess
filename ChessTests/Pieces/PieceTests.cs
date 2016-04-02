using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Game;
using Chess.Util;

namespace Chess.Pieces.Tests
{
    [TestClass()]
    public class PieceTests
    {
   
        [TestMethod()]
        public void TryMoveToTest()
        {
            Piece piece = new Pawn(Enums.Color.White);
            piece.CurrSquare = new Square(4, 4);

            Square toSquare = new Square(3,4);
            toSquare.CurrPiece = new Pawn(Enums.Color.White);

            if(piece.TryMoveTo(toSquare)) Assert.Fail();

            toSquare.CurrPiece = null;
            if (!piece.TryMoveTo(toSquare)) Assert.Fail();
        }
    }
}