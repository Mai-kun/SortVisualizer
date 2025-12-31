using System.Collections;
using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;

namespace SortingAlgorithm;

public class BubbleSort : ISorter
{
    private const int ArraySize = 50;
    private int _currentI;
    private int _currentJ;
    private int[] _values = null!;

    public BubbleSort()
    {
        Reset();
    }

    public string Name { get; set; } = "Bubble Sort";
    public int Comparisons { get; private set; }
    public int Swaps { get; private set; }
    public bool IsFinished { get; private set; }

    public void Reset()
    {
        _currentI = 0;
        _currentJ = 0;
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
            for (_currentJ = 0; _currentJ < count - _currentI - 1; _currentJ++)
            {
                Comparisons++;
                if (_values[_currentJ] > _values[_currentJ + 1])
                {
                    Swaps++;
                    (_values[_currentJ], _values[_currentJ + 1]) = (_values[_currentJ + 1], _values[_currentJ]);
                }

                yield return null;
            }
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

    public void Draw(int screenWidth, int screenHeight)
    {
        var barWidth = screenWidth / ArraySize;

        for (var i = 0; i < ArraySize; i++)
        {
            var color = GetBarColor(i);
            Vector2 position = new(i * barWidth, screenHeight - _values[i]);
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

        if (index == _currentJ || index == _currentJ + 1)
        {
            return Color.Red;
        }

        if (index >= ArraySize - _currentI)
        {
            return Color.Green;
        }

        return Color.White;
    }
}