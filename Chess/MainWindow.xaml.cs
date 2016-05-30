using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Util;
using System.Windows.Media;
using System.ComponentModel;
using Chess.Pieces;
using System.Windows.Data;
using Chess.Logic;
using System;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        private Player pWhite, pBlack;
        private Game game;
        private Square currSquare;
        private Label currLabel;
        private Label[,] labels = new Label[8, 8];
        private BindingList<Label> bindings = new BindingList<Label>();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = "CurrUnicode";
        }

        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label clickedLabel = (Label)e.Source;
            int column = Grid.GetColumn(clickedLabel);
            int row = Grid.GetRow(clickedLabel);

            Square clickedSquare = game.GetSquare(row, column);

            Click(clickedSquare, clickedLabel);

            if (currSquare != null && currLabel != null)
            {
                if (currSquare.IsClicked())
                {
                    if (game.TryMove(currSquare, clickedSquare))
                    {
                        ShowMove(currSquare, clickedLabel);
                    }
                }
                Click(currSquare, currLabel);
            }
            currSquare = clickedSquare;
            currLabel = clickedLabel;

            //Debug.WriteLine("currSquare =  {0}", currSquare);
            //Debug.WriteLine("currLabel = {0}", currLabel);

            UpdateGui();
        }

        private void Click(Square currSquare, Label label)
        {
            bool isClicked = currSquare.Click();
            // TODO sometimes doesnt update
            if (currSquare.CurrPiece != null && game.CurrPlayer.Color == currSquare.CurrPiece.Color)
            {
                label.Foreground = isClicked ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.Black);
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        private void ShowMove(Square currSquare, Label clickedLabel)
        {
            clickedLabel.Content = currSquare.CurrUnicode;
            currLabel.Content = "";
            currSquare.RemovePiece();
        }

        private void NewGame()
        {
            pWhite = new Player(white_textBox.Text, Enums.Color.White);
            pBlack = new Player(black_textBox.Text, Enums.Color.Black);

            if (game == null)
            {
                game = Game.GetInstance();
            }
            this.game = game.NewGame(pWhite, pBlack);

            //ClearGui();
            InitNewBoard();
        }

        private void InitNewBoard()
        {
            InitSquares();

            SetupPositions(Enums.Color.Black, pBlack);
            SetupPositions(Enums.Color.White, pWhite);
        }

        // todo: clear clicked etc 
        private void ClearGui()
        {
            foreach (var child in chessGrid.Children)
            {
                Label label = child as Label;
                label.Content = "";
            }
            UpdateGui();
        }

        private void UpdateGui()
        {
            chessGrid.UpdateLayout();
        }

        private void SetupPositions(Enums.Color color, Player player)
        {
            int col = 0;
            int row = Enums.Color.Black == color ? 0 : Game.MaxRow - 1;

            game.AddPiece(new Rook(color), player, row, col++);
            game.AddPiece(new Knight(color), player, row, col++);
            game.AddPiece(new Bishop(color), player, row, col++);
            game.AddPiece(new Queen(color), player, row, col++);
            game.AddPiece(new King(color), player, row, col++);
            game.AddPiece(new Bishop(color), player, row, col++);
            game.AddPiece(new Knight(color), player, row, col++);
            game.AddPiece(new Rook(color), player, row, col++);

            row = Enums.Color.Black == color ? 1 : Game.MaxRow - 2;
            for (col = 0; col < Game.MaxColumn; col++)
            {
                game.AddPiece(new Pawn(color), player, row, col);
            }
        }

        private void InitSquares()
        {
            int i = 0;
            int maxRow = Game.MaxRow;
            int maxCol = Game.MaxColumn;

            var children = chessGrid.Children;
            Square[,] squares = new Square[maxRow, maxCol];

            for (int row = 0; row < maxRow; row++)
            {
                bool evenRow = (row % 2 == 0);

                for (int column = 0; column < maxCol; column++)
                {
                    bool evenColumn = (column % 2 == 0);

                    Enums.Color color;
                    if (evenRow && evenColumn || !evenRow && !evenColumn)
                        color = Enums.Color.White;
                    else
                        color = Enums.Color.Black;

                    Square square = new Square(row, column, color);
                    Label label = children[i++] as Label;

                    squares[row, column] = square;
                    labels[row, column] = label;
                    bindings.Add(label);
                }
            }
            game.Squares = squares;
            chessGrid.DataContext = bindings;

            BindLabels(squares);

        }

        private void BindLabels(Square[,] squares)
        {
            int r = 0, c = 0;
            foreach (Label label in bindings)
            {
                if (r < 8)
                    DataBind(squares[r, c], label);
                c++;
                if (c % 8 == 0)
                {
                    r++;
                    c = 0;
                }
            }
        }

        private void DataBind(Square square, Label label)
        {
            var binding = new Binding(square.CurrUnicode);
            binding.Source = square;

            label.DataContext = square;
            label.SetBinding(ContentProperty, binding);
        }


    }
}