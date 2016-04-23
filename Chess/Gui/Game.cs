using System;
using Chess.Pieces;
using Chess.Util;
using System.Data;

namespace Chess.Gui
{
    public class Game
    {
        public static readonly int TotalRows = 8, TotalColumns = 8;
        private static Game instance;     

        private Player white, black;
        public Player White => white;
        public Player Black => black;        

        public DataTable table { get; set; }

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
        }


        public void AddPieceToSquare(Piece piece, int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square != null) square.CurrPiece = piece;
        }

        public Square GetSquare(int row, int column)
        {
            return (Square)table.Rows[row][column];
        }

        internal bool ClickSquare(int row, int column)
        {
            return table == null ? false : GetSquare(row, column).CurrPiece.Click();
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
    }
}