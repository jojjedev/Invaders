using SFML.System;
using SFML.Graphics;


namespace Invaders;

public class HomeScene : Scene
{
    private Text _newGame;
    private Text _scoreboard;
    private Text _quit;
    private List<Text> _texts;
    public HomeScene() : base()
    {
        _entities = new List<Entity>();
        _texts = new List<Text>();
        _gui = new GUI();
        CreateObjects();
    }

    private void CreateObjects()
    {
        Background background = new Background();
        uint CharacterSize = 45;
        Color color = Color.White;
        _entities.Add(background);
        background.Create(this);
        _newGame = _gui.CreateText(this, "New Game", CharacterSize, color);
        _texts.Add(_newGame);
        _scoreboard = _gui.CreateText(this, "Scoreboard", CharacterSize, color);
        _texts.Add(_scoreboard);
        _quit = _gui.CreateText(this, "Quit", CharacterSize, color);
        _texts.Add(_quit);
    }

    public override void RenderAll(RenderTarget target)
    {
        base.RenderAll(target);
        for (int i = 0; i < _texts.Count; i++)
        {
            Text text = _texts[i];
            FloatRect bounds = text.GetGlobalBounds();
            text.Origin = new Vector2f(bounds.Width * 0.5f, bounds.Height * 0.5f);
            text.Position = new Vector2f(256, 512 + 2f * bounds.Height * (i - 1));
            target.Draw(text);
        }
    }
    
}