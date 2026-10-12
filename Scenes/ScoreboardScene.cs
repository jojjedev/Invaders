using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public class ScoreboardScene : Scene
{
    private bool _keyPressed;
    private ScoreboardGUI _gui;
    public ScoreboardScene() : base()
    {
        _gui = new ScoreboardGUI(this);
        _background.Color = new Color(64, 173, 115);
        Spawn(_background);
        Spawn(_gui);
        Console.WriteLine(_entities);
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

    protected override void LoadNextScene(int target)
    {
        switch (target)
        {
            case 1:
                Events.PublishSceneChange(SceneName.Game);
                break;
            case 2:
                Events.PublishSceneChange(SceneName.Home);
                break;
        }
    }
    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        if (!_keyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter)) LoadNextScene(_gui.Target());
        
        IsUpOrDownPressed();
    }
}