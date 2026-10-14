using System.Security.Principal;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

class Program
{
    static void Main(string[] args)
    {
        // TODO Fixa inladdning av textures.
        using (var window = new RenderWindow(new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Invaders"))
        {
            Game game = new Game();
            window.Closed += (o, e) => window.Close();
            window.SetFramerateLimit(60);
            Clock clock = new Clock();
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f); 
                game.CurrentScene.UpdateAll(dt);
                if (game.GameState != 1) window.Close();
                window.Clear();
                game.CurrentScene.RenderAll(window);
                window.Display();
            }
        }
    }
}