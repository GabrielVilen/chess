using Chess.Pieces;
using Chess.Util;

namespace Chess.Game
{
    internal class Game
    {
        private static Game instance;

        private Player playerOne, playerTwo;
        public Player PlayerOne => playerOne;
        public Player PlayerTwo => playerTwo;

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

        public void NewGame(Player playerOne, Player playerTwo)
        {
            GetInstance();

            this.playerOne = playerOne;
            this.playerTwo = playerTwo;

            board = new Board();
        }

        public void Score(Piece destPiece)
        {
            Player scorer, loser;
            if (playerOne.Color == destPiece.Color)
            {
                scorer = playerTwo;
                loser = playerOne;
            }
            else
            {
                scorer = playerOne;
                loser = playerTwo;
            }

            scorer.Score += (int) destPiece.PieceType;
            loser.RemovePiece(destPiece);
        }

        public bool InCheck(Enums.Color color, Square destSquare)
        {
            Player checkPlayer = (playerOne.Color == color) ? playerOne : playerTwo;

            return !checkPlayer.InCheck() && !checkPlayer.WillBeInCheck(destSquare);
        }
    }
}