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
    public class RookTests
    {
        [TestMethod()]
        public void CanMoveToTest()
        {
            Piece rook = new Rook(Enums.Color.Black);
            rook.CurrSquare = new Square(4, 4);

            if (!rook.CanMoveTo(new Square(4, 7))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(4, 2))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(2, 4))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(8, 4))) Assert.Fail();

            if (rook.CanMoveTo(new Square(5, 5))) Assert.Fail();

            var board = Game.Game.GetInstance().Board;

            board.AddPieceToSquare(new Pawn(Enums.Color.White), 4, 6);
            if (!rook.CanMoveTo(new Square(4, 5))) Assert.Fail();
            if (rook.CanMoveTo(new Square(4, 7))) Assert.Fail();

            board.AddPieceToSquare(new Pawn(Enums.Color.White), 2, 4);
            if (rook.CanMoveTo(new Square(1, 4))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(3, 4))) Assert.Fail();

            board.AddPieceToSquare(new Pawn(Enums.Color.White), 7, 4);
            if (rook.CanMoveTo(new Square(8, 4))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(5, 4))) Assert.Fail();

            board.AddPieceToSquare(new Pawn(Enums.Color.White), 4, 2);
            if (rook.CanMoveTo(new Square(4, 1))) Assert.Fail();
            if (!rook.CanMoveTo(new Square(4, 3))) Assert.Fail();
        }
    }
}