using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Chess.Util;
using System.Windows.Media;
using System.ComponentModel;
using Chess.Pieces;
using System.Windows.Data;
using Chess.Logic;

/*

Project: Chess
Last edited date: 2016-06-05
Developer: Gabriel Vilén

*/
namespace Chess
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
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

        /// <summary>
        ///     Called when the user clicks on the chess grid, the grid containg the chess game.
        ///     Gets the currently clicked label and square, calls the method for clicking on it and 
        ///     tries to move to the square. Finnaly updates the gui.
        /// </summary>
        private void chessGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label clickedLabel = (Label)e.Source;

                int column = Grid.GetColumn(clickedLabel);
                int row = Grid.GetRow(clickedLabel);

                Square square = game.GetSquare(row, column);

                Click(square, clickedLabel);
                TryMoveTo(square, clickedLabel);

                UpdateGui();
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     Tries to move to the given square, if succesful shows the changes on the gui. 
        /// </summary>
        private void TryMoveTo(Square square, Label clickedLabel)
        {
            if (currSquare != null && currLabel != null)
            {
                if (currSquare.IsClicked())
                {
                    if (game.TryMove(currSquare, square))
                    {
                        ShowMove(clickedLabel, currSquare);
                    }
                }
                Click(currSquare, currLabel);
            }
            currSquare = square;
            currLabel = clickedLabel;
        }

        /// <summary>
        ///     Clicks on the given square and changes the color of the given label according to 
        ///     if the square is clicked or not.
        /// </summary>
        private void Click(Square square, Label label)
        {
            bool isClicked = square.Click();

            if (square.CurrPiece != null && game.CurrPlayer.Color == square.CurrPiece.Color)
            {
                label.Foreground = isClicked ? new SolidColorBrush(Colors.Gray) : new SolidColorBrush(Colors.Black);
            }
            else
            {
                label.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void newGameButton_Click(object sender, RoutedEventArgs e)
        {
            NewGame();
        }

        /// <summary>
        ///     Shows the move on the gui by changing the given label to the content of the given square. 
        ///     Also removes the piece at the given square.
        /// </summary>
        private void ShowMove(Label clickedLabel, Square square)
        {
            clickedLabel.Content = square.CurrUnicode;
            currLabel.Content = "";
            square.RemovePiece();
        }

        /// <summary>
        ///     Called when a new game is to be created. Gets a new instance of the game with the parameters.
        /// </summary>
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

        /// <summary>
        ///     Inits a new board by calling methods for setting up standard positions of the pieces.
        /// </summary>
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

        /// <summary>
        ///     Set up the positions of the pieces in the game to the standard chess setup.
        ///     The pieces are created with the given color and added to the given player.
        /// </summary>
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

        /// <summary>
        ///     Loops and creates the squares in the game by alternating black and white color. 
        ///     Also binds the squares to the labels with the same row and column number.
        /// </summary>
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

        /// <summary>
        ///     Loops through the given squares and binds them to the labels in the gui. 
        /// </summary>
        /// <param name="squares"></param>
        private void BindLabels(Square[,] squares)
        {
            int r = 0, c = 0;
            foreach (Label label in bindings)
            {
                if (r < 8)
                    DataBind(label, squares[r, c]);
                c++;
                if (c % 8 == 0)
                {
                    r++;
                    c = 0;
                }
            }
        }

        /// <summary>
        ///     Data binds the given label to the unicode of the given square to enable dynamic view-model updates.
        /// </summary>
        private void DataBind(Label label, Square square)
        {
            var binding = new Binding(square.CurrUnicode);
            binding.Source = square;

            label.DataContext = square;
            label.SetBinding(ContentProperty, binding);
        }


    }
}