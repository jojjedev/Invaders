using SFML.Audio;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public sealed class ScoreboardScene : Scene
{
    private ScoreboardGUI _gui;
    private Sound _music;
    public ScoreboardScene() : base()
    {
        _gui = new ScoreboardGUI(this);
        Background.SetColor(new Color(64, 173, 115));
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
            case 1:
                Events.PublishSceneChange(SceneName.Game);
                break;
            case 2:
                Events.PublishSceneChange(SceneName.Home);
                Sound back = Assets.LoadAudio("select_006");
                back.Play();
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
        if (!KeyPressed)
        {
            _gui.UpdateTarget();
        }
        if (Keyboard.IsKeyPressed(Enter) && Timer > 0.5f) LoadNextScene(_gui.Target());
        
        IsAnyKeyPressed();
        base.UpdateAll(dt);

    }
}