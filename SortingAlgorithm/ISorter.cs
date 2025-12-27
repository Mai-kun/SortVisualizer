using System.Collections;

namespace SortingAlgorithm;

public interface ISorter
{
    bool IsFinished { get; }

    IEnumerator Sort();

    void Draw(int screenWidth, int screenHeight);

    void Reset();
}