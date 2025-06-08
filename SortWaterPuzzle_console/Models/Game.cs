using System;
using System.Collections.Generic;
using System.Linq;

namespace SortWaterPuzzle_console.Models
{
    public class Game
    {
        private readonly List<Tube> _tubes = new();

        public Game()
        {
            Init();
        }

        public List<Tube> GetTubes() => _tubes;

        private void Init()
        {
            var rng = new Random();

            // количество пробирок (от 5 до 12)
            int totalTubes = rng.Next(5, 13);

            // количество пустых пробирок (1 или 2)
            int emptyTubes = rng.Next(1, 3);

            // Рассчитываем количество заполненных пробирок
            int filledTubes = totalTubes - emptyTubes;

            // количество цветов равно количеству заполненных пробирок
            int numColors = filledTubes;

            // все возможные цвета, исключая None
            var availableColors = Enum.GetValues(typeof(WaterColor))
                .Cast<WaterColor>()
                .Where(c => c != WaterColor.None)
                .ToList();

            // случайно выбираем numColors уникальных цветов
            var selectedColors = availableColors.OrderBy(_ => rng.Next()).Take(numColors).ToList();
            
            var allColors = selectedColors.SelectMany(color => Enumerable.Repeat(color, 4)).ToList();
            
            allColors = allColors.OrderBy(_ => rng.Next()).ToList();
            _tubes.Clear();

            //  создаём пробирки
            for (int i = 0; i < totalTubes; i++)
            {
                _tubes.Add(new Tube());
            }

            // распределяем цвета по заполненным пробиркам
            int colorIndex = 0;
            for (int i = 0; i < filledTubes; i++)
            {
                int remainingColors = allColors.Count - colorIndex;
                int maxCapacity = Math.Min(4, remainingColors);
                
                int layers = rng.Next(1, maxCapacity + 1);
                
                var colors = allColors.Skip(colorIndex).Take(layers).ToList();
                _tubes[i].Fill(colors);
                colorIndex += layers;
            }

            // распределяем оставшиеся цвета
            while (colorIndex < allColors.Count)
            {
                for (int i = 0; i < filledTubes && colorIndex < allColors.Count; i++)
                {
                    if (_tubes[i].AvailableSpace > 0)
                    {
                        // Console.WriteLine($"Filling tube {i}");
                        _tubes[i].Fill(_tubes[i].Contents.Concat(new[] { allColors[colorIndex] }));
                        colorIndex++;
                    }
                }
            }
        }

        public bool IsWon()
        {
            return _tubes.All(t => t.IsSorted());
        }
    }
}