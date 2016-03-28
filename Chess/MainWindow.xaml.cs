using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitChessBoard();
        }

        private void InitChessBoard()
        {
            Debug.WriteLine("Children:");


            foreach (var child in ChessGrid.Children)
            {
                Rectangle rec = new Rectangle();
                child.
           
                Debug.WriteLine(child.ToString());
            }
        }
    }
}
