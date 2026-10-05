using SFML.Window;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

class Program
{
    static void Main(string[] args)
    {
        using (var window = new RenderWindow(new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Invaders"))
        {
            window.Closed += (o, e) => window.Close();
            Clock clock = new Clock();
            while (window.IsOpen)
            {
                window.DispatchEvents();

                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f);
                // TODO UPDATE
                window.Clear();
                // TODO RENDER
                window.Display();
            }
        }
    }
}