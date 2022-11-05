using Solitaire.Models;
using System;
using System.Collections.Generic;
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

namespace Solitaire.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private const int BoardWidth = 7;
        private const int BoardHeight = 7;
        //private GameButtonView[,] _gameButtons = new GameButtonView[BoardWidth, BoardHeight];
        private Game _game;

        public GameView()
        {
            _game = new Game(new Board1());
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < BoardWidth; i++)
                BoardGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < BoardHeight; i++)
                BoardGrid.RowDefinitions.Add(new RowDefinition());

            for (int x = 0; x < BoardWidth; x++)
            {
                for (int y = 0; y < BoardHeight; y++)
                {
                    if ((x < 2 && y < 2) ||
                        (x < 2 && y > 4) ||
                        (x > 4 && y < 2) ||
                        (x > 4 && y > 4)
                        )
                        continue;

                    GameButtonView button = new GameButtonView(new Position(x, y));
                    Grid.SetColumn(button, x);
                    Grid.SetRow(button, y);
                    BoardGrid.Children.Add(button);
                    //_gameButtons[x, y] = button;
                }
            }
        }
    }
}
