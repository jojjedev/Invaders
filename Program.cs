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
        SceneManager sceneManager = new SceneManager();
        using (var window = new RenderWindow(new VideoMode(SCREEN_WIDTH, SCREEN_HEIGHT), "Invaders"))
        {
            /*window.Resized += (o, e) =>
            {
                FloatRect view = new FloatRect(0, 0, window.Size.X, window.Size.Y);
                window.SetView(new View(view));
            }; */
            window.Closed += (o, e) => window.Close();
            window.SetFramerateLimit(60);
            Clock clock = new Clock();
            
            while (window.IsOpen)
            {
                window.DispatchEvents();

                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f); 
                sceneManager.CurrentScene.UpdateAll(dt);
                window.Clear();
                sceneManager.CurrentScene.RenderAll(window);
                window.Display();
            }
        }
    }
}