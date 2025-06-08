using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace SortWaterPuzzle_console
{
    public class WaterColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is not WaterColor color)
                return Brushes.Transparent;
            
            return color switch
            {
                WaterColor.Red => Brushes.Red,
                WaterColor.Blue => Brushes.Blue,
                WaterColor.Green => Brushes.Green,
                WaterColor.Yellow => Brushes.Yellow,
                WaterColor.Purple => Brushes.Purple,
                WaterColor.Orange => Brushes.Orange,
                WaterColor.Pink => Brushes.Pink,
                WaterColor.Cyan => Brushes.Cyan,
                WaterColor.Magenta => Brushes.Magenta,
                WaterColor.Lime => Brushes.LimeGreen,
                WaterColor.Brown => Brushes.Brown,
                _ => Brushes.Transparent
            };
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}