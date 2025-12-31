using System.Collections;
using Raylib_CSharp.Transformations;

namespace SortingAlgorithm;

public interface ISorter
{
    public string Name { get; set; }
    public int Comparisons { get; }
    public int Swaps { get; }
    public bool IsFinished { get; }

    public IEnumerator Sort();
    public void Reset();
    public void Draw(Rectangle drawArea);
}