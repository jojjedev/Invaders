using System.ComponentModel;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GUI : Entity
{
    protected List<Text> _texts;
    protected int _currentTarget;
    protected uint _defaultCharacterSize = 45;
    protected  Color _defaultColor = new Color(100,100,100);
    public GUI() : base("sheet")
    {
        _sprite.Position = new Vector2f(600, 0);
    }

    protected virtual Text CreateText(Scene scene, string text, uint characterSize, Color color)
    {
        Text newText = new Text();
        newText.Font = scene.Assets.LoadFont("kenvector_future_thin");
        newText.CharacterSize = characterSize;
        newText.DisplayedString = text;
        newText.FillColor = color;
        return newText;
    }

    public virtual void UpdateTarget()
    {
        
    }
    public virtual int Target()
    {
        Text target = _texts[_currentTarget];
        target.CharacterSize = 50;
        target.FillColor = Color.White;
        return _texts.IndexOf(target);
    }

    
}