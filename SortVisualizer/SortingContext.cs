using System.Collections;
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

    public string CurrentAlgorithmName => _sorter.GetType().Name;

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
        if (!_sorter.IsFinished)
        {
            _sortingProcess.MoveNext();
        }
    }

    public void Draw(int screenWidth, int screenHeight)
    {
        _sorter.Draw(screenWidth, screenHeight);
    }
}