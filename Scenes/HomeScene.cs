using System.Security.Cryptography.X509Certificates;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.SceneName;


namespace Invaders;

public class HomeScene : Scene
{
    private HomeGUI _gui;
    public HomeScene()
    {
        _gui = new HomeGUI(this);
        Spawn(_background);
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
    
    
    public override void UpdateAll(float dt)
    {
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            Entity entity = _entities[i];
            entity.Update(this, dt);
        }

        for (int i = 0; i < _entities.Count; i++)
        {
            Entity entity = _entities[i];
            if (entity.Dead) _entities.RemoveAt(i);
            else i++;
        }
        if (!_keyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter))
        {
            LoadNextScene(_gui.Target());
        }
        IsUpOrDownPressed();
        base.UpdateAll(dt);

    }
}