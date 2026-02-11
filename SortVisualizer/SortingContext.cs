using System.Collections;
using Raylib_CSharp.Transformations;
using SortingAlgorithm;

namespace SortVisualizer;

public class SortingContext
{
    private ISorter _sorter = null!;
    private IEnumerator _sortingProcess = null!;

    public SortingContext(ISorter initialSorter)
    {
        SetSorter(initialSorter);
    }

    public string AlgorithmName => _sorter.Name;
    public int Comparisons => _sorter.Comparisons;
    public int Swaps => _sorter.Swaps;
    private bool IsFinished => _sorter.IsFinished;

    public void SetSorter(ISorter sorter)
    {
        _sorter = sorter ?? throw new ArgumentNullException(nameof(sorter));
        Restart();
    }

    public void Restart()
    {
        _sorter.Reset();
        _sortingProcess = _sorter.Sort();
    }

    public void Update()
    {
        if (!IsFinished)
        {
            _sortingProcess.MoveNext();
        }
    }

    public void Draw(Rectangle drawArea)
    {
        _sorter.Draw(drawArea);
    }
}