using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Util;

namespace Chess.Game.Tests
{
    [TestClass()]
    public class SquareTests
    {
        private Square s;

        [TestMethod()]
        public void SquareTest()
        {
            s = new Square(10, 10, Enums.Color.Black);
         //   Debug.WriteLine(arr[0,0]);
         //   if(Assert.Fail();
        }

     //   [TestMethod()]
        public void IsValidTest()
        {
            Assert.Fail();
        }

    //    [TestMethod()]
        public void MoveTest()
        {
            Assert.Fail();
        }
    }
}