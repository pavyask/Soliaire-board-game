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
        private MarbleCell _cell;

        private Game _game;

        public Position Position => _cell.Position;

        //private bool _isSelected;

        //public bool IsSelected
        //{
        //    get => _cell.IsSelected;
        //    set => SetProperty(ref _isSelected, value);
        //}

        //private bool _isEmpty;

        //public bool IsEmpty
        //{
        //    get => _cell.IsEmpty;
        //    set => SetProperty(ref _isEmpty, value);
        //}

        public bool IsSelected => _cell.IsSelected;

        public bool IsEmpty => _cell.IsEmpty;

        public GameButtonViewModel(MarbleCell cell, Game game)
        {
            _cell = cell;
            _game = game;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSelected), nameof(IsEmpty))]
        private string _test = "";

        [RelayCommand]
        private void MoveAttempt()
        {
            Test += "TEST ";
            Console.WriteLine($"IsSelected: {IsSelected}, IsEmpty: {IsEmpty}, Position: {Position}");
            _game.MoveAttempt(Position);
            Console.WriteLine($"IsSelected: {IsSelected}, IsEmpty: {IsEmpty}, Position: {Position}");
        }
    }
}
