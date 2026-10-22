using System.ComponentModel;
using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class GUI : IOnScreenObject
{
    protected List<Text> Texts;
    protected int CurrentTarget;
    protected uint DefaultCharacterSize = 45;
    protected  Color DefaultColor = new Color(100,100,100);

    public bool Dead { get; set; }
    public bool DontClearOnLoad { get; set; }
    protected virtual Text CreateText(Scene scene, string text, uint characterSize, Color color)
    {
        Text newText = new Text();
        newText.Font = scene.Assets.LoadFont("LTBinaryNeue-Bold");
        newText.CharacterSize = characterSize;
        newText.DisplayedString = text;
        newText.FillColor = color;
        return newText;
    }

    public virtual void UpdateTarget() { }
    public virtual int Target()
    {
        Text target = Texts[CurrentTarget];
        target.CharacterSize = 50;
        target.FillColor = Color.White;
        return Texts.IndexOf(target);
    }
    public virtual void Create(Scene scene) { }
    public virtual void Destroy() { }
    public virtual void Update(float dt) { }
    public virtual void Render(RenderTarget target) { }
}