using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public class ScoreboardScene : Scene
{
    private ScoreboardGUI _gui;
    public ScoreboardScene() : base()
    {
        _gui = new ScoreboardGUI(this);
        _background.Color = new Color(64, 173, 115);
        Spawn(_background);
        Spawn(_gui);
    }

    protected override void LoadNextScene(int target)
    {
        Clear();
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
        if (!_keyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter) && _timer > 0.5f) LoadNextScene(_gui.Target());
        
        IsUpOrDownPressed();
        base.UpdateAll(dt);

    }
}