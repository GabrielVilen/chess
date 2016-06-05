/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess.Util
{
    /// <summary>
    ///     Class containng enums used in the chess game.
    /// </summary>
    public class Enums
    {
        /// <summary>
        ///     Represents the different type of pieces in the game, with their chess value as integers.
        /// </summary>
        public enum PieceType
        {
            None = 0,
            Pawn = 1,
            Knight = 3,
            Bishop = 3,
            Rook = 5,
            Queen = 9,
            King
        }

        /// <summary>
        ///     Represents the black and white color of pieces and players. 
        /// </summary>
        public enum Color
        {
            Black,
            White
        }
    }
}