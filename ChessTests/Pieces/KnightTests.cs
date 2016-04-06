using Chess.Game;
using Chess.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Pieces.Tests
{
    [TestClass()]
    public class KnightTests
    {
        [TestMethod()]
        public void CanMoveToTest()
        {
            Piece knight = new Knight(Enums.Color.Black);
            knight.CurrSquare = new Square(4, 4);

            if (!knight.CanMoveTo(new Square(2, 3))) Assert.Fail();
            if (!knight.CanMoveTo(new Square(2, 5))) Assert.Fail();

            if (!knight.CanMoveTo(new Square(3, 2))) Assert.Fail();
            if (!knight.CanMoveTo(new Square(3, 6))) Assert.Fail();

            if (!knight.CanMoveTo(new Square(5, 2))) Assert.Fail();
            if (!knight.CanMoveTo(new Square(5, 6))) Assert.Fail();

            if (!knight.CanMoveTo(new Square(6, 3))) Assert.Fail();
            if (!knight.CanMoveTo(new Square(6, 5))) Assert.Fail();

            if (knight.CanMoveTo(new Square(6, 6))) Assert.Fail();

/*            Piece pawn = new Pawn(Enums.Color.Black);
            Game.Board.GetInstance().AddPieceToSquare(pawn, 6, 5);
            if (knight.CanMoveTo(new Square(6, 5))) Assert.Fail();*/
        }
    }
}