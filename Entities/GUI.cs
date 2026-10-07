using System.ComponentModel;
using SFML.Graphics;

namespace Invaders;

public class GUI : Entity
{
    public GUI() : base("sheet")
    {
        
    }

    public Text CreateText(Scene scene, string text, uint characterSize, Color color)
    {
        Text newText = new Text();
        newText.Font = scene.Assets.LoadFont("kenvector_future_thin");
        newText.CharacterSize = characterSize;
        newText.DisplayedString = text;
        newText.FillColor = color;
        return newText;
    }
}