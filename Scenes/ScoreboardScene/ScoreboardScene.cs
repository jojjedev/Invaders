using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public sealed class ScoreboardScene : Scene
{
    private ScoreboardGUI _gui;
    public ScoreboardScene() : base()
    {
        _gui = new ScoreboardGUI(this);
        Background.SetColor(new Color(64, 173, 115));
        Spawn(Background);
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
        if (!KeyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter) && Timer > 0.5f) LoadNextScene(_gui.Target());
        
        IsAnyKeyPressed();
        base.UpdateAll(dt);

    }
}