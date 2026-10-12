using SFML.Graphics;

namespace Invaders;

public class GameScene : Scene
{
    public GameScene()
    {
        _background.Color = Color.Cyan;
        Spawn(_background);
    }
}