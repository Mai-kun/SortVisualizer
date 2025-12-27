using System;
using Raylib_CSharp;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using SortingAlgorithm;

namespace SortVisualizer;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 450;
        Window.Init(screenWidth, screenHeight, "Sorting Visualizer");
        Time.SetTargetFPS(60);

        var context = new SortingContext(new BubbleSort());

        float timer = 0;
        const float delay = 0.01f;
        var isPaused = false;

        while (!Window.ShouldClose())
        {
            if (Input.IsKeyPressed(KeyboardKey.R))
            {
                context.Restart();
            }

            if (Input.IsKeyPressed(KeyboardKey.Space))
            {
                isPaused = !isPaused;
            }

            if (!isPaused)
            {
                timer += Time.GetFrameTime();
                if (timer >= delay)
                {
                    context.Update();
                    timer = 0;
                }
            }

            Graphics.BeginDrawing();

            Graphics.ClearBackground(Color.Black);
            context.Draw(screenWidth, screenHeight);
            Graphics.DrawText(context.CurrentAlgorithmName, 5, 5, 24, Color.White);

            Graphics.EndDrawing();

        }

        Window.Close();
    }
}