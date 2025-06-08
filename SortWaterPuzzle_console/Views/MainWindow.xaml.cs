using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SortWaterPuzzle_console.ViewModels;

namespace SortWaterPuzzle_console.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}