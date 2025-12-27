using System.Collections;
using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;

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

    public bool IsFinished { get; private set; }

    public void Reset()
    {
        _currentI = 0;
        _currentJ = 0;
        _values = new int[ArraySize];
        var rand = new Random();
        for (var k = 0; k < ArraySize; k++)
        {
            _values[k] = rand.Next(10, 400);
        }

        IsFinished = false;
    }

    public IEnumerator Sort()
    {
        var count = _values.Length;
        for (_currentI = 0; _currentI < count - 1; _currentI++)
        {
            for (_currentJ = 0; _currentJ < count - _currentI - 1; _currentJ++)
            {
                if (_values[_currentJ] > _values[_currentJ + 1])
                {
                    (_values[_currentJ], _values[_currentJ + 1]) = (_values[_currentJ + 1], _values[_currentJ]);
                }

                yield return null;
            }
        }

        IsFinished = true;
    }

    public void Draw(int screenWidth, int screenHeight)
    {
        var barWidth = screenWidth / ArraySize;
        for (var i = 0; i < ArraySize; i++)
        {
            var color = Color.White;

            if (IsFinished)
            {
                color = Color.Green;
            }
            else
            {
                if (i == _currentJ || i == _currentJ + 1)
                {
                    color = Color.Red;
                }
                else if (i >= ArraySize - _currentI)
                {
                    color = Color.Green;
                }
            }

            Graphics.DrawRectangleV(
                new Vector2(i * barWidth, screenHeight - _values[i]),
                new Vector2(barWidth - 2, _values[i]),
                color
            );
        }
    }
}