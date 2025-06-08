using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SortWaterPuzzle_console.Models;
using System.Collections.ObjectModel;

namespace SortWaterPuzzle_console.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private Game _game;
        private Tube? _selectedTube;

        [ObservableProperty]
        private ObservableCollection<Tube> _tubes;

        [ObservableProperty]
        private string _statusText = "Выберите первую пробирку";

        public MainWindowViewModel()
        {
            _game = new Game();
            Tubes = new ObservableCollection<Tube>(_game.GetTubes());
        }

        [RelayCommand]
        private void NewGame()
        {
            _game = new Game();
            _selectedTube = null;
            StatusText = "Новая игра началась! Выберите первую пробирку";
            
            Tubes.Clear();
            foreach (var tube in _game.GetTubes())
            {
                Tubes.Add(tube);
            }
        }

        [RelayCommand]
        private void SelectTube(Tube tube)
        {
            if (_selectedTube != null)
                _selectedTube.IsSelected = false;

            if (_selectedTube == null)
            {
                tube.IsSelected = true;
                _selectedTube = tube;
                StatusText = "Теперь выберите целевую пробирку";
            }
            else if (_selectedTube == tube)
            {
                _selectedTube = null;
                StatusText = "Выбор отменен";
            }
            else
            {
                tube.IsSelected = false;
        
                if (_selectedTube.CanPourInto(tube))
                {
                    _selectedTube.PourInto(tube);
                    StatusText = "Ход выполнен!";

                    if (_game.IsWon())
                    {
                        StatusText = "Поздравляем! Вы выиграли!";
                    }
                }
                else
                {
                    StatusText = "Невозможно перелить! Попробуйте другую пробирку";
                }
        
                _selectedTube = null;
            }
        }
    }
}