using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;

namespace Invaders;

public class HomeGUI : GUI
{
    private Text _newGame;
    private Text _scoreboard;
    private Text _quit;
    private List<Text> _texts;
    private uint _defaultCharacterSize = 45;
    private Color _defaultColor = new Color(100,100,100);
    private int _currentTarget;
    
    public HomeGUI(HomeScene scene) : base()
    {
        _texts = new List<Text>();
        CreateObjects(scene);
    }
    private void CreateObjects(HomeScene scene)
    {
        uint CharacterSize = _defaultCharacterSize;
        Color color = _defaultColor;
        _newGame = CreateText(scene, "New Game", CharacterSize, color);
        _texts.Add(_newGame);
        _scoreboard = CreateText(scene, "Scoreboard", CharacterSize, color);
        _texts.Add(_scoreboard);
        _quit = CreateText(scene, "Quit", CharacterSize, color);
        _texts.Add(_quit);
        _currentTarget = 0;
        Target();
    }
    public int Target()
    {
        Text target = _texts[_currentTarget];
        target.CharacterSize = 50;
        target.FillColor = Color.White;
        return _texts.IndexOf(target);
    }
    public void UpdateTarget()
    {
        if (Keyboard.IsKeyPressed(Down) || Keyboard.IsKeyPressed(S))
        {
            _texts[_currentTarget].CharacterSize = _defaultCharacterSize;
            _texts[_currentTarget].FillColor = _defaultColor;
            _currentTarget++;
            if (_currentTarget == _texts.Count) _currentTarget = 0;
            Target();
        }
        if (Keyboard.IsKeyPressed(Up) || Keyboard.IsKeyPressed(W))
        {
            _texts[_currentTarget].CharacterSize = _defaultCharacterSize;
            _texts[_currentTarget].FillColor = _defaultColor;
            _currentTarget--;
            if (_currentTarget == -1) _currentTarget = _texts.Count - 1;
            Target();
        }

    }

    public override void Render(RenderTarget target)
    {
        for (int i = 0; i < _texts.Count; i++)
        {
            Text text = _texts[i];
            FloatRect bounds = text.GetGlobalBounds();
            text.Origin = new Vector2f(bounds.Width * 0.5f, bounds.Height * 0.5f);
            text.Position = new Vector2f(256, 512 + 2f * bounds.Height * (i - 1));
            target.Draw(text);
        }
    }

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        
    }
}