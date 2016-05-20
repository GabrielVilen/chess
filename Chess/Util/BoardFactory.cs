using System;
using Chess.Gui;
using Chess.Pieces;
using Chess.Util;

namespace Chess
{
    internal class BoardFactory
    {
        internal static void SetupNewBoard(Game game)
        {
            for(int row = 0; row < MainWindow.TotalRows; row++)
            {
                for (int col = 0; col < MainWindow.TotalColumns; col++)
                {
                    game.AddPieceToSquare(new None(), row, col);
                }
            }
            game.AddPieceToSquare(new King(Enums.Color.Black), 1, 3);
            game.AddPieceToSquare(new King(Enums.Color.White), 6, 4);
        }
    }
}