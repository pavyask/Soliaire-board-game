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
    /// Interaction logic for GameButtonView.xaml
    /// </summary>
    public partial class GameButtonView : UserControl
    {
        public GameButtonView(Position pos)
        {
            InitializeComponent();
            Button button = new Button()
            {
                Content = pos.ToString(),
                Style = FindResource("GameButtonStyle") as Style,
            };

            AddChild(button);
        }
    }
}
