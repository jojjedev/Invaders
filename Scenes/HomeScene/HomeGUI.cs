using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;
using static Invaders.Constants;
namespace Invaders;

public sealed class HomeGUI : GUI
{
    private readonly HomeScene _currentScene;
    public HomeGUI(HomeScene scene)
    {
        _currentScene = scene;
        Texts = new List<Text>();
        CreateMenuTexts();
    }
    private void CreateMenuTexts()
    {
        uint characterSize = DefaultCharacterSize;
        Color color = DefaultColor;
        Texts.Add(CreateText(_currentScene, "New Game", characterSize, color));
        Texts.Add(CreateText(_currentScene, "Scoreboard", characterSize, color));
        Texts.Add(CreateText(_currentScene, "Quit", characterSize, color));
        CurrentTarget = 0;
        Target();
    }
    
    public override void UpdateTarget()
    {
        if (Keyboard.IsKeyPressed(Down) || Keyboard.IsKeyPressed(S))
        {
            Texts[CurrentTarget].CharacterSize = DefaultCharacterSize;
            Texts[CurrentTarget].FillColor = DefaultColor;
            CurrentTarget++;
            if (CurrentTarget == Texts.Count) CurrentTarget = 0;
            Target();
            PlayChangeTargetSound();
        }
        if (Keyboard.IsKeyPressed(Up) || Keyboard.IsKeyPressed(W))
        {
            Texts[CurrentTarget].CharacterSize = DefaultCharacterSize;
            Texts[CurrentTarget].FillColor = DefaultColor;
            CurrentTarget--;
            if (CurrentTarget == -1) CurrentTarget = Texts.Count - 1;
            Target();
            PlayChangeTargetSound();
        }
        
    }

    private void PlayChangeTargetSound()
    {
        Sound sound = _currentScene.Assets.LoadAudio("select_002");
        sound.Volume = 25;
        sound.Play();
    }
    public override void Render(RenderTarget target)
    {
        for (int i = 0; i < Texts.Count; i++)
        {
            Text text = Texts[i];
            FloatRect bounds = text.GetGlobalBounds();
            text.Origin = new Vector2f(bounds.Width * 0.5f, bounds.Height * 0.5f);
            text.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f + 2f * bounds.Height * (i - 1));
            target.Draw(text);
        }
    }
}