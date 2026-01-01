using System.Collections;
using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;

namespace SortingAlgorithm;

public class SelectionSort : ISorter
{
    private const int ArraySize = 50;
    private int _currentI;
    private int _currentJ;
    private int _minIndex;
    private int[] _values = null!;


    public string Name => "Selection Sort";
    public int Comparisons { get; private set; }
    public int Swaps { get; private set; }
    public bool IsFinished { get; private set; }

    public void Reset()
    {
        _currentI = 0;
        _currentJ = 0;
        _minIndex = 0;
        _values = new int[ArraySize];
        var rand = new Random();
        for (var k = 0; k < ArraySize; k++)
        {
            _values[k] = rand.Next(10, 350);
        }

        Comparisons = 0;
        Swaps = 0;
        IsFinished = false;
    }

    public IEnumerator Sort()
    {
        var count = _values.Length;
        for (_currentI = 0; _currentI < count - 1; _currentI++)
        {
            _minIndex = _currentI;
            for (_currentJ = _currentI + 1; _currentJ < count; _currentJ++)
            {
                Comparisons++;
                if (_values[_currentJ] < _values[_minIndex])
                {
                    _minIndex = _currentJ;
                }

                yield return null;
            }

            if (_minIndex == _currentI)
            {
                continue;
            }

            Swaps++;
            (_values[_currentI], _values[_minIndex]) = (_values[_minIndex], _values[_currentI]);
            yield return null;
        }

        IsFinished = true;
    }

    public void Draw(Rectangle drawArea)
    {
        var barWidth = drawArea.Width / ArraySize;

        for (var i = 0; i < ArraySize; i++)
        {
            var color = GetBarColor(i);

            var posX = drawArea.X + i * barWidth;
            var posY = drawArea.Y + drawArea.Height - _values[i];

            Vector2 position = new(posX, posY);
            Vector2 size = new(barWidth - 2, _values[i]);

            Graphics.DrawRectangleV(position, size, color);
        }
    }

    private Color GetBarColor(int index)
    {
        if (IsFinished)
        {
            return Color.Green;
        }

        if (index == _currentI)
        {
            return Color.Yellow;
        }

        if (index == _minIndex)
        {
            return Color.Orange;
        }

        if (index == _currentJ)
        {
            return Color.Red;
        }

        if (index < _currentI)
        {
            return Color.Green;
        }

        return Color.White;
    }
}