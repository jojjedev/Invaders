using SFML.Graphics;

namespace Invaders;

public class ScoreboardScene : Scene
{
    public ScoreboardScene() : base()
    {
        _background.Color = Color.Green;
        Spawn(_background);
        Console.WriteLine(_entities);
    }
}