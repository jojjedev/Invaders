using System.Security.Cryptography.X509Certificates;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.SceneName;


namespace Invaders;

public class HomeScene : Scene
{
    private bool _keyPressed;
    private HomeGUI _gui;
    public HomeScene()
    {
        _gui = new HomeGUI(this);
        Spawn(_gui);
    }
    private void LoadNextScene(int target)
    {
        Clear();
        switch (target)
        {
            case 0:
                Loader.Load(Game);
                Events.PublishSceneChange(Game);
                break;
            case 1:
                Loader.Load(Scoreboard);
                Events.PublishSceneChange(Scoreboard);
                break;
            case 2:
                Loader.Load(Quit);
                Events.PublishSceneChange(Quit);
                break;
        }
    }
    private void IsUpOrDownPressed()
    {
        if (Keyboard.IsKeyPressed(Down) ||
             Keyboard.IsKeyPressed(S) ||
             Keyboard.IsKeyPressed(Up) ||
             Keyboard.IsKeyPressed(W))
        {
            _keyPressed = true;
        }
        else _keyPressed = false;
    }
    
    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
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
            //TODO Trigga event för att ladda ny scene från SceneManager
            LoadNextScene(_gui.Target());
        }
        IsUpOrDownPressed();
        
    }
}