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

        public Square[,] squares { get; set; }

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

        public Game NewGame(Player white, Player black)
        {
            instance = new Game();           

            this.white = white;
            this.black = black;

            InitSquares();

            return instance;
        }

        private void InitSquares()
        {
            squares = new Square[TotalRows, TotalColumns];

            for (int row = 0; row < TotalRows; row++)
            {
                bool evenRow = (row % 2 == 0);

                for (int column = 0; column < TotalColumns; column++)
                {
                    bool evenColumn = (column % 2 == 0);

                    Enums.Color color;
                    if (evenRow && evenColumn || !evenRow && !evenColumn)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    Square square = new Square(row, column, color); // start at row and column +1 ?

                    squares[row, column] = square;
                }
            }
        }

        public void AddPieceToSquare(Piece piece, int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square != null) square.CurrPiece = piece;
        }

        public Square GetSquare(int row, int column)
        {
            if (squares == null) return null;
            return squares[row, column];
        }

        internal bool ClickSquare(int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square == null || square.CurrPiece == null || square.CurrPiece.PieceType == Enums.PieceType.None)
                return false;

            return square.CurrPiece.Click();
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