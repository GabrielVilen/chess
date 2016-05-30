using Chess.Pieces;
using Chess.Util;
using System.ComponentModel;
using System.Diagnostics;
using System;

namespace Chess.Logic
{
    public class Square
    {
        public int Row { get; }
        public int Column { get; }
        public Enums.Color Color { get; }
        public Piece CurrPiece { get; private set; }
        private string currUnicode;

        public string CurrUnicode
        {
            get { return currUnicode; }
            set
            {
                currUnicode = value;
            }
        }

        public Square(int row, int column, Enums.Color color)
        {
            Row = row;
            Column = column;
            Color = color;
        }

        public bool CanPlace(Piece piece)
        {
            return CurrPiece == null ? true : CurrPiece.Color != piece.Color || CurrPiece.PieceType == Enums.PieceType.None;
        }

        public void SetPiece(Piece piece)
        {
            CurrPiece = piece;
            CurrUnicode = piece.Unicode;
        }

        public Piece Move(Piece piece)
        {
            Piece oldPiece = CurrPiece;
            CurrPiece = piece;

            return oldPiece;
        }

        public bool IsEmpty()
        {
            return CurrPiece == null ? true : CurrPiece.PieceType == Enums.PieceType.None;
        }

        public bool IsSame(Square clickedSquare)
        {
            return (Row == clickedSquare.Row) && (Column == clickedSquare.Column);
        }

        public override string ToString()
        {
            return CurrPiece != null ? CurrPiece.Unicode : "";
        }

        internal void RemovePiece()
        {
            if(CurrPiece != null)
            {
                CurrPiece = null;
                CurrUnicode = "";
            }
        }

        internal bool IsClicked()
        {
            return CurrPiece == null ? false : CurrPiece.IsClicked;
        }

        internal bool Click()
        {
            return CurrPiece == null ? false : CurrPiece.Click();
        }

    }
}