using System.Security.Cryptography.X509Certificates;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.SceneName;


namespace Invaders;

public sealed class HomeScene : Scene
{
    private readonly HomeGUI _gui;
    public HomeScene()
    {
        _gui = new HomeGUI(this);
        Spawn(Background);
        Spawn(_gui);

    }
    private void LoadNextScene(int target)
    {
        Clear();
        switch (target)
        {
            case 0:
                Events.PublishSceneChange(SceneName.Game);
                break;
            case 1:
                Events.PublishSceneChange(Scoreboard);
                break;
            case 2:
                Events.PublishSceneChange(Quit);
                break;
        }
    }
    private void IsAnyKeyPressed()
    {
        if (Keyboard.IsKeyPressed(Down) ||
            Keyboard.IsKeyPressed(S) ||
            Keyboard.IsKeyPressed(Up) ||
            Keyboard.IsKeyPressed(W) ||
            Keyboard.IsKeyPressed(Enter))
        {
            KeyPressed = true;
        }
        else KeyPressed = false;
    }
    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        if (!KeyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter) && Timer > 0.5f)
        {
            LoadNextScene(_gui.Target());
        }
        IsAnyKeyPressed();
    }
}