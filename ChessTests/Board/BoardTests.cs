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
            for (int i = 0; i < 8; i++)
            {
                Debug.WriteLine("");
                Debug.Write("row: " + i + "  ");
                for (int j = 0; j < 8; j++)
                {
                    Debug.Write(" " + s[i, j].Color);
                }
            }
        }
    }
}