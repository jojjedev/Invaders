using SFML.Graphics;

namespace Invaders;

public sealed class GameScene : Scene
{
    private GameGUI _gui;
    public GameScene()
    {
        Background.SetColor(Color.Cyan);
        _gui = new GameGUI();
        Spawn(Background);
        Spawn(_gui);
    }
}