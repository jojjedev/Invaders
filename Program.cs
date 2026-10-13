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
            SceneManager sceneManager = new SceneManager();
            window.Closed += (o, e) => window.Close();
            window.SetFramerateLimit(60);
            Clock clock = new Clock();
            
            while (window.IsOpen)
            {
                window.DispatchEvents();
                float dt = clock.Restart().AsSeconds();
                dt = MathF.Min(dt, 0.01f); 
                sceneManager.CurrentScene.UpdateAll(dt);
                if (sceneManager.GameState != 1) window.Close();
                window.Clear();
                sceneManager.CurrentScene.RenderAll(window);
                window.Display();
            }
        }
    }
}