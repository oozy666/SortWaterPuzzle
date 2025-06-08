using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace SortWaterPuzzle_console.Views.Converters
{
    public class SelectedToBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
                return Brushes.LightBlue;
            
            return Brushes.Transparent;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}