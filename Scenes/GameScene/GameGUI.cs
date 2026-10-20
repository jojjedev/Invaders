using SFML.System;
using SFML.Graphics;


namespace Invaders;

public class GameGUI : GUI
{
    private int _score;
    private int _health;
    public GameGUI(GameScene scene)
    {
        Texts = new List<Text>();
        CreateObjects(scene);
        DefaultCharacterSize = 20;
    }

    private void CreateObjects(GameScene scene)
    {
        uint CharacterSize = DefaultCharacterSize;
        Color color = DefaultColor;
        Texts.Add(CreateText(scene,$"Score: {_score}", CharacterSize, color));
        Texts.Add(CreateText(scene, $"Health: {_health}", CharacterSize, color));
    }
    private void IncreaseScore()
    {
        _score++;
        Console.WriteLine(_score);
    }

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
    }
}