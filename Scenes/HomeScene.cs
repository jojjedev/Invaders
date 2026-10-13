using System.Security.Cryptography.X509Certificates;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.SceneName;


namespace Invaders;

public sealed class HomeScene : Scene, IKeyCheck
{
    private HomeGUI _gui;
    public HomeScene()
    {
        _gui = new HomeGUI(this);
        Spawn(Background);
        Spawn(_gui);

    }
    protected override void LoadNextScene(int target)
    {
        Clear();
        switch (target)
        {
            case 0:
                //Loader.SceneChange(this, Game);
                Events.PublishSceneChange(Game);
                break;
            case 1:
                //Loader.SceneChange(this, Scoreboard);
                Events.PublishSceneChange(Scoreboard);
                break;
            case 2:
                //Loader.SceneChange(this,Quit);
                Events.PublishSceneChange(Quit);
                break;
        }
    }
    public void IsAnyKeyPressed()
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