using System.Collections;
using Raylib_CSharp.Transformations;

namespace SortingAlgorithm;

public interface ISorter
{
    public string Name { get; }
    public int Comparisons { get; }
    public int Swaps { get; }
    public bool IsFinished { get; }

    public void Reset();
    public IEnumerator Sort();
    public void Draw(Rectangle drawArea);
}