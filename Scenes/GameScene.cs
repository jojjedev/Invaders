using SFML.Graphics;

namespace Invaders;

public sealed class GameScene : Scene
{
    public GameScene()
    {
        Background.Color = Color.Cyan;
        Spawn(Background);
    }
}