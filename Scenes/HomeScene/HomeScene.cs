using System.Security.Cryptography.X509Certificates;
using SFML.Audio;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.SceneName;


namespace Invaders;

public sealed class HomeScene : Scene
{
    private readonly HomeGUI _gui;
    private readonly Sound _music;
    public HomeScene()
    {
        _gui = new HomeGUI(this);
        Spawn(Background);
        Spawn(_gui);
        _music = Assets.PlaySound("menu", 10, true);
    }
    
    private void LoadNextScene(int target)
    {
        Clear();
        _music.Dispose();
        switch (target)
        {
            case 0:
                Events.PublishSceneChange(Game);
                break;
            case 1:
                Events.PublishSceneChange(Scoreboard);
                Assets.PlaySound("select_005", 100, false);
                break;
            case 2:
                Events.PublishSceneChange(Quit);
                Assets.PlaySound("select_006", 100, false);
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