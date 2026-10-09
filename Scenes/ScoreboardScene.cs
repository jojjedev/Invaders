using SFML.Graphics;

namespace Invaders;

public class ScoreboardScene : Scene
{
    public ScoreboardScene() : base()
    {
        _background.Color = Color.Green;
        Console.WriteLine(_entities.Count);
    }
}