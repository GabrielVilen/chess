using Chess.Pieces;
using Chess.Util;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Logic
{
    /// <summary>
    ///     Class that contains the main logic for interacting with the players and squares in the chess game.
    /// </summary>
    public class Game
    {
        public const int MaxRow = 8, MaxColumn = 8;
        private static Game instance;

        private Player white, black;
        public Player CurrPlayer { get; private set; }
        public Player Opponent => CurrPlayer == white ? black : white;

        public Square[,] Squares { private get; set; }

        /// <summary>
        ///     Singleton that creates new game instance if current is null
        /// </summary>
        public static Game GetInstance()
        {
            if (instance == null)
                instance = new Game();

            return instance;
        }

        private Game()
        {
        }

        /// <summary>
        ///     Creates a new instance of the game with the given white and black player.
        /// </summary>
        public Game NewGame(Player white, Player black)
        {
            instance = new Game(); // todo look over

            instance.white = white;
            instance.black = black;
            instance.CurrPlayer = white;

            return instance;
        }

        /// <summary>
        ///     Adds the given piece to the given player at the given row and column.
        /// </summary>
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

        /// <summary>
        ///     Returns the square at the given row and column in the current game.
        /// </summary>
        public Square GetSquare(int row, int column)
        {
            if (row >= MaxRow) row--;
            if (column >= MaxColumn) column--;
            if (row < 0) row++;
            if (column < 0) column++;

            return Squares[row, column];
        }

        /// <summary>
        ///     Clicks the square at the given row and column, if it is not null and has an existing piece.
        /// </summary>
        internal bool ClickSquare(int row, int column)
        {
            Square square = GetSquare(row, column);
            if (square == null || square.CurrPiece == null || square.CurrPiece.PieceType == Enums.PieceType.None)
                return false;

            return square.CurrPiece.Click();
        }

        /// <summary>
        ///     Tries to move the current piece at the square "fromSquare" to the given square "toSquare".
        /// </summary>
        /// <returns></returns>
        public bool TryMove(Square fromSquare, Square toSquare)
        {
            bool hasMoved = CurrPlayer.TryMove(fromSquare, toSquare);
            if (hasMoved)
            {
                CurrPlayer = (CurrPlayer == white ? black : white);
                toSquare.SetPiece(fromSquare.CurrPiece);
            }

            return hasMoved;
        }

        /// <summary>
        ///     Scores the given piece to the opponent of the piece (e.g. if piece is black, the white player scores).
        /// </summary>
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
            Player inCheckPlayer = (player == white ? black : white);
            inCheckPlayer.InCheck = inCheck;
        }

        /// <summary>
        ///     Returns the opponent of the given player; if the given player is white, black is returned and vise verse. 
        /// </summary>
        public Player GetOpponent(Player player)
        {
            return (player == white ? black : white);
        }
    }
}