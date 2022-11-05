using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solitaire.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Solitaire.ViewModels
{
    public partial class GameButtonViewModel : ObservableObject
    {

        private readonly MarbleCell _cell;

        public bool IsSelected => _cell.IsSelected;

        public bool IsEmpty => _cell.IsEmpty;

        public Position Position => _cell.Position;

        public GameButtonViewModel(MarbleCell cell)
        {
            _cell = cell;
        }

        [RelayCommand]
        private void MoveAttempt()
        {
            MessageBox.Show($"Move attempt: {Position}");
        }
    }
}
