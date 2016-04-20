using System;
using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    public class Game
    {
        private static Game instance;

        private Player white, black;
        public Player White => white;
        public Player Black => black;

        private Board board;
        public Board Board => board;

        /// <summary>
        ///     Singleton that creates new game instance if current is null
        /// </summary>
        /// <returns>Get singleton</returns>
        public static Game GetInstance()
        {
            if (instance == null)
                instance = new Game();

            return instance;
        }

        private Game()
        {
        }

        public void NewGame(Player white, Player black)
        {
            GetInstance();

            this.white = white;
            this.black = black;

            board = new Board();
        }

        public void Score(Piece destPiece)
        {
            Player scorer, loser;
            if (white.Color == destPiece.Color)
            {
                scorer = black;
                loser = white;
            }
            else
            {
                scorer = white;
                loser = black;
            }

            scorer.Score += (int) destPiece.PieceType;
            loser.RemovePiece(destPiece);
        }

        public bool InCheck(Enums.Color color, Square destSquare)
        {
            Player checkPlayer = (white.Color == color) ? white : black;

            return !checkPlayer.inCheck && !checkPlayer.CanCheck(destSquare);
        }

        internal bool ClickSquare(int row, int column)
        {
            return board == null ? false : board.ClickSquare(row, column);
        }
    }
}