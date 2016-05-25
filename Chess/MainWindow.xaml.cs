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

        //public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = "CurrUnicode";
            // OnPropertyChanged("CurrUnicode");
        }

        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (game == null) return;

            Label newLabel = (Label)e.Source;
            int column = Grid.GetColumn(newLabel);
            int row = Grid.GetRow(newLabel);

            //test
            newLabel = labels[row, column];            

            if (currSquare != null && currLabel != null)
            {
                if (currSquare.IsClicked())
                {
                    game.TryMove(currSquare, row, column);
                }
                Click(currSquare, currLabel);
            }
            currSquare = game.GetSquare(row, column);
            currLabel = newLabel;

            Click(currSquare, currLabel);

            UpdateGui();
        }

        private void NewGame()
        {
            Debug.WriteLine("NEW GAME");
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
            // UpdateGui();
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

                    DataBind(square, label);

                    squares[row, column] = square;
                    labels[row, column] = label;
                }
            }
            game.Squares = squares;
        }

        private void DataBind(Square square, Label label)
        {
            var binding = new Binding(square.CurrUnicode);


            binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            binding.NotifyOnSourceUpdated = true;
            binding.NotifyOnTargetUpdated = true;
            //binding.XPath = square.CurrUnicode;
            binding.Mode = BindingMode.OneWay;

            binding.Source = square;

            label.DataContext = square;
            Debug.WriteLine("{0}.DataContext = {1}", label, label.DataContext);

            label.SetBinding(ContentProperty, binding);

            //label.TargetUpdated += OnTargetUpdated; // todo called only once

            Debug.WriteLine("binding: {0} --> {1},{2} hash: {3} ", label.Name, ((Square)binding.Source).Row, ((Square)binding.Source).Column, ((Square)binding.Source).GetHashCode());
        }

        // TODO: IS NEVER CALLED
        private void OnTargetUpdated(object sender, DataTransferEventArgs args)
        {
            Debug.WriteLine("OnTargetUpdated(object {0}, DataTransferEventArgs {1})", sender.ToString(), args.ToString());

        }

        private void Click(Square currSquare, Label label)
        {
            bool isClicked = currSquare.Click();
            label.Foreground = isClicked ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.Black);
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }
    }
}