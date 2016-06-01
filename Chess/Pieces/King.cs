using System;
using Chess.Util;
using Chess.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chess.Pieces
{
    public class King : Piece
    {
        public King(Enums.Color color) : base(Enums.PieceType.King, color)
        {
            Unicode = (color == Enums.Color.Black ? Unicodes.King_black : Unicodes.King_white);
        }

        public override bool CanMoveTo(Square clickedSquare)
        {
            return (Math.Abs(currRow - clickedSquare.Row) < 2) && (Math.Abs(currColumn - clickedSquare.Column) < 2)
                && !IsInCheck(clickedSquare);
        }
        
        // TODO: om i shack måste man flytta kungen, implementera shack 
        private bool IsInCheck(Square clickedSquare)
        {
            var pieces = Game.GetInstance().Opponent.Pieces;
            foreach (Piece piece in pieces)
            {
                if (piece is Pawn)
                {
                    if (((Pawn)piece).CanCapture(clickedSquare))
                    {
                        return true;
                    }                  
                }
                else if (piece.CanMoveTo(clickedSquare))
                {
                    return true;
                }
            }
            return false;
        }
    }
}