using System;
using Chess.Pieces;
using Chess.Util;
using System.Diagnostics;

namespace Chess.Logic
{
    public class Game
    {
        public const int MaxRow = 8, MaxColumn = 8;
        private static Game instance;

        private Player white, black;
        public Player CurrPlayer { get; set; }
        public Player Opponent { get { return CurrPlayer == white ? black : white; } }

        public Square[,] Squares { get; set; }

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
            instance = new Game(); // todo look over

            instance.white = white;
            instance.black = black;
            instance.CurrPlayer = white;

            return instance;
        }

        public void AddPiece(Piece piece, Player player, int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square != null)
            {
                square.SetPiece(piece);
                player.AddPiece(piece);
                piece.CurrSquare = square;
            }
        }

        public Square GetSquare(int row, int column)
        {
            if (row >= MaxRow) row--;
            if (column >= MaxColumn) column--;
            if (row < 0) row++;
            if (column < 0) column++;

            return Squares[row, column];
        }


        internal bool ClickSquare(int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square == null || square.CurrPiece == null || square.CurrPiece.PieceType == Enums.PieceType.None)
                return false;

            return square.CurrPiece.Click();
        }

        public bool TryMove(Square fromSquare, Square square)
        {
            bool hasMoved = CurrPlayer.TryMove(fromSquare, square);
            if (hasMoved)
            {
                CurrPlayer = (CurrPlayer == white ? black : white);
                square.SetPiece(fromSquare.CurrPiece);
            }

            return hasMoved;
        }


        public bool TryMove(Square fromSquare, int toRow, int toCol)
        {
            return TryMove(fromSquare, GetSquare(toRow, toCol));
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

            scorer.Score += (int)destPiece.PieceType;
            loser.RemovePiece(destPiece);
        }

        /// <summary>
        ///     Sets the opponent player of the given player to the given bool inCheck. 
        ///     E.g. if the given player is white, the black player is set to inCheck. 
        /// </summary>
        internal void SetOpponentInCheck(Player player, bool inCheck)
        {
            Debug.WriteLine("SetOpponentInCheck({0},{1})", player, inCheck);
            Player inCheckPlayer = (player == white ? black : white);
            inCheckPlayer.InCheck = inCheck;
        }
    }
}