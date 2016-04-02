using System.Diagnostics;
using Chess.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Chess.Board.Tests
{
    [TestClass()]
    public class BoardTests
    {
        private Game.Board b;
        private Square[,] s;

        public BoardTests()
        {
            b = Game.Board.GetInstance();
            s = b.Squares;
        }

        [TestMethod()]
        public void SquareLengthTest()
        {
            if (s.Length != 64)
                Assert.Fail();
        }

        [TestMethod()]
        public void SquareColorTest()
        {
            for (int i = 0; i < Game.Board.Rows; i++)
            {
                Debug.WriteLine("");
                Debug.Write("row: " + s[i, 0].Row + "  ");
                for (int j = 0; j < Game.Board.Columns; j++)
                {
                    Debug.Write(" " + s[i, j].Color);
                }
            }
        }
    }
}