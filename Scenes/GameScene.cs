using SFML.Graphics;

namespace Invaders;

public sealed class GameScene : Scene
{
    private GameGUI _gui;
    public GameScene()
    {
        Background.Color = Color.Cyan;
        _gui = new GameGUI();
        Spawn(Background);
        Spawn(_gui);
    }
}