using System.ComponentModel;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class GUI : Entity
{
    public GUI() : base("sheet")
    {
        _sprite.Position = new Vector2f(600, 0);
    }

    public virtual Text CreateText(Scene scene, string text, uint characterSize, Color color)
    {
        Text newText = new Text();
        newText.Font = scene.Assets.LoadFont("kenvector_future_thin");
        newText.CharacterSize = characterSize;
        newText.DisplayedString = text;
        newText.FillColor = color;
        return newText;
    }
}