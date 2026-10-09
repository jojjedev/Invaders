using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public class HomeScene : Scene
{
    private Text _newGame;
    private Text _scoreboard;
    private Text _quit;
    private List<Text> _texts;
    private uint _defaultCharacterSize = 45;
    private Color _defaultColor = new Color(100,100,100);
    private int _currentTarget;
    private bool _keyPressed;
    private float timer;
    public HomeScene() : base()
    {
        _entities = new List<Entity>();
        _texts = new List<Text>();
        CreateObjects();
    }

    private void CreateObjects()
    {
        Background background = new Background();
        _gui = new GUI();
        uint CharacterSize = _defaultCharacterSize;
        Color color = _defaultColor;
        _entities.Add(background);
        _entities.Add(_gui);
        background.Create(this);
        _newGame = _gui.CreateText(this, "New Game", CharacterSize, color);
        _texts.Add(_newGame);
        _scoreboard = _gui.CreateText(this, "Scoreboard", CharacterSize, color);
        _texts.Add(_scoreboard);
        _quit = _gui.CreateText(this, "Quit", CharacterSize, color);
        _texts.Add(_quit);
        Target(_texts[0]);
        _currentTarget = 0;
    }

    private void Target(Text target)
    {
        target.CharacterSize = 50;
        target.FillColor = Color.White;
    }

    private void UpdateTarget()
    {
        
        if (Keyboard.IsKeyPressed(Down) || Keyboard.IsKeyPressed(S))
        {
            _texts[_currentTarget].CharacterSize = _defaultCharacterSize;
            _texts[_currentTarget].FillColor = _defaultColor;
            _currentTarget++;
            if (_currentTarget == _texts.Count) _currentTarget = 0;
            Text target = _texts[_currentTarget];
            Target(target);
        }
        if (Keyboard.IsKeyPressed(Up) || Keyboard.IsKeyPressed(W))
        {
            _texts[_currentTarget].CharacterSize = _defaultCharacterSize;
            _texts[_currentTarget].FillColor = _defaultColor;
            _currentTarget--;
            if (_currentTarget == -1) _currentTarget = _texts.Count - 1;
            Text target = _texts[_currentTarget];
            Target(target);
        }

    }

    private void IsUpOrDownPressed()
    {
        if ((Keyboard.IsKeyPressed(Down) ||
             Keyboard.IsKeyPressed(S) ||
             Keyboard.IsKeyPressed(Up) ||
             Keyboard.IsKeyPressed(W)))
        {
            _keyPressed = true;
        }
        else _keyPressed = false;
    }

    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        if (!_keyPressed)
        {
            UpdateTarget();
        }
        IsUpOrDownPressed();
        
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