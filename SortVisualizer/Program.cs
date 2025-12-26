using System.Numerics;
using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;

namespace SortVisualizer;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        const int arraySize = 50;
        var values = new float[arraySize];
        var rand = new Random();
        for (var k = 0; k < arraySize; k++)
        {
            values[k] = rand.Next(10, 400);
        }

        var i = 0;
        var j = 0;
        float timer = 0;
        const float delay = 0.01f;
        var sorted = false;

        const int screenWidth = 800;
        const int screenHeight = 450;
        Window.Init(screenWidth, screenHeight, "Sorting Visualizer");
        Time.SetTargetFPS(60);

        while (!Window.ShouldClose())
        {
            if (!sorted)
            {
                timer += Time.GetFrameTime();
                if (timer >= delay)
                {
                    if (values[j] > values[j + 1])
                    {
                        (values[j], values[j + 1]) = (values[j + 1], values[j]);
                    }

                    j++;


                    if (j >= arraySize - i - 1)
                    {
                        j = 0;
                        i++;
                    }

                    if (i >= arraySize - 1)
                    {
                        sorted = true;
                    }

                    timer = 0;
                }
            }


            Graphics.BeginDrawing();
            Graphics.ClearBackground(Color.Black);

            const float barWidth = (float)screenWidth / arraySize;
            for (var k = 0; k < arraySize; k++)
            {
                var color = Color.White;

                if (k == j || k == j + 1)
                {
                    color = Color.Red;
                }

                if (sorted)
                {
                    color = Color.Green;
                }

                Graphics.DrawRectangleV(
                    new Vector2(k * barWidth, screenHeight - values[k]),
                    new Vector2(barWidth - 2, values[k]),
                    color
                );
            }

            Graphics.EndDrawing();
        }

        Window.Close();
    }
}


