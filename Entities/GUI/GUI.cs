using System.ComponentModel;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class GUI : Entity
{
    protected List<Text> Texts;
    protected int CurrentTarget;
    protected uint DefaultCharacterSize = 45;
    protected  Color DefaultColor = new Color(100,100,100);
    protected GUI() : base("sheet")
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
        Text target = Texts[CurrentTarget];
        target.CharacterSize = 50;
        target.FillColor = Color.White;
        return Texts.IndexOf(target);
    }

    
}