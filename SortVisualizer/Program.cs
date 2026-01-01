using Raylib_CSharp;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Interact;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;
using Raylib_CSharp.Windowing;
using SortingAlgorithm;

namespace SortVisualizer;

internal static class Program
{
    [STAThread]
    public static void Main()
    {
        const int screenWidth = 800;
        const int screenHeight = 500;
        const int topPanelHeight = 60;
        Window.Init(screenWidth, screenHeight, "Sorting Visualizer");
        Time.SetTargetFPS(60);

        var availableSorters = new List<ISorter>
        {
            new BubbleSort(),
            new InsertionSort(),
            new SelectionSort(),
        };

        var context = new SortingContext(availableSorters.First());

        var isDropdownOpen = false;
        var dropdownButtonRec = new Rectangle(10, 10, 200, 40);

        float timer = 0;
        const float delay = 0.03f;
        var isPaused = false;

        while (!Window.ShouldClose())
        {
            var mousePos = Input.GetMousePosition();
            var isMouseClicked = Input.IsMouseButtonPressed(MouseButton.Left);

            if (isMouseClicked)
            {
                if (ShapeHelper.CheckCollisionPointRec(mousePos, dropdownButtonRec))
                {
                    isDropdownOpen = !isDropdownOpen;
                }
                else if (isDropdownOpen)
                {
                    for (var i = 0; i < availableSorters.Count; i++)
                    {
                        var itemRec = new Rectangle(dropdownButtonRec.X,
                            dropdownButtonRec.Y + dropdownButtonRec.Height * (i + 1), dropdownButtonRec.Width,
                            dropdownButtonRec.Height);

                        if (!ShapeHelper.CheckCollisionPointRec(mousePos, itemRec))
                        {
                            continue;
                        }

                        context.SetSorter(availableSorters[i]);
                        isDropdownOpen = false;
                    }
                }
            }

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

            var sortArea = new Rectangle(0, topPanelHeight, screenWidth, screenHeight - topPanelHeight);
            context.Draw(sortArea);

            Graphics.DrawRectangle(0, 0, screenWidth, topPanelHeight, Color.DarkGray);
            var statsX = (int)dropdownButtonRec.Width + 30;
            Graphics.DrawText($"Algorithm: {context.AlgorithmName}", statsX, 10, 20, Color.White);
            Graphics.DrawText($"Comparisons: {context.Comparisons}", statsX, 35, 15, Color.LightGray);
            Graphics.DrawText($"Swaps/Inserts: {context.Swaps}", statsX + 150, 35, 15, Color.LightGray);

            Graphics.DrawRectangleRec(dropdownButtonRec, Color.Gray);
            Graphics.DrawRectangleLinesEx(dropdownButtonRec, 2, Color.Black);
            Graphics.DrawText("Select algorithm", (int)(dropdownButtonRec.X + 10), (int)(dropdownButtonRec.Y + 10), 20,
                Color.LightGray);

            if (isDropdownOpen)
            {
                for (var i = 0; i < availableSorters.Count; i++)
                {
                    var itemRec = new Rectangle(dropdownButtonRec.X,
                        dropdownButtonRec.Y + dropdownButtonRec.Height * (i + 1), dropdownButtonRec.Width,
                        dropdownButtonRec.Height);

                    var isHover = ShapeHelper.CheckCollisionPointRec(mousePos, itemRec);
                    Graphics.DrawRectangleRec(itemRec, isHover ? Color.DarkGray : Color.Gray);
                    Graphics.DrawRectangleLinesEx(itemRec, 1, Color.White);

                    Graphics.DrawText(availableSorters[i].Name, (int)itemRec.X + 10, (int)itemRec.Y + 10, 20,
                        Color.White);
                }
            }

            Graphics.EndDrawing();

        }

        Window.Close();
    }
}