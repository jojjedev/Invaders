using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public class ScoreboardGUI : GUI
{
    private Dictionary<Text, Text[]> _scores;
    private Color _scoreColor;
    public ScoreboardGUI(ScoreboardScene scene) : base()
    {
        _texts = new List<Text>();
        _scores = new Dictionary<Text, Text[]>();
        _defaultCharacterSize = 32;
        _scoreColor = new Color(168, 154, 226);
        CreateScoreboard(scene);
    }

    private void CreateTitleAndBack(ScoreboardScene scene)
    {
        Text title = CreateText(scene, "Scoreboard", 50, Color.Cyan);
        FloatRect titleBounds = title.GetGlobalBounds();
        title.Origin = new Vector2f(titleBounds.Width * 0.5f, titleBounds.Height * 0.5f);
        title.Position = new Vector2f(256, titleBounds.Height * 2f);
        _texts.Add(title);
        Text newGame = CreateText(scene, "New Game", 40, _defaultColor);
        _texts.Add(newGame); // Index 1
        Text back = CreateText(scene, "Back", 40, _defaultColor);
        _texts.Add(back); // Index 2
        _currentTarget = 1; // Sätter target på "Back" by default.
        Target();
    }

    private void CreateScoreboard(ScoreboardScene scene)
    {
        uint characterSize = _defaultCharacterSize;
        CreateTitleAndBack(scene);
        for (int i = 1; i <= 10; i++)
        {
            _scores[CreateText(scene, $"{i}.", characterSize, _scoreColor)] =
                StoreHighScore(scene, "Johan", $"{i * 200}");
            // TODO: Input name ska komma från en funktion som frågar spelaren om sitt namn.
        }
    }


    public Text[] StoreHighScore(Scene scene, string name, string score)
    {
        Text[] playerScore = new Text[]
        {
            CreateText(scene, name, _defaultCharacterSize, _scoreColor),             
            CreateText(scene, score, _defaultCharacterSize, _scoreColor)

        };
        return playerScore;
    }

    public override int Target()
    {
        Text target = _texts[_currentTarget];
        target.CharacterSize = 45;
        target.FillColor = Color.White;
        return _texts.IndexOf(target);
    }

    public override void UpdateTarget()
    {
        if (Keyboard.IsKeyPressed(Down) || 
            Keyboard.IsKeyPressed(S) || 
            Keyboard.IsKeyPressed(Up) || 
            Keyboard.IsKeyPressed(W))
        {
            _texts[_currentTarget].CharacterSize = 40;
            _texts[_currentTarget].FillColor = _defaultColor;
            _currentTarget = _currentTarget == 1 ? _currentTarget = 2 : _currentTarget = 1;
            Target();
        }
        
    }

    public override void Render(RenderTarget target)
    {
        int placement = 0;
        foreach (var pair in _scores)
        {
            placement++;
            Text number = pair.Key;
            FloatRect numberBounds = number.GetGlobalBounds();
            number.Origin = new Vector2f(numberBounds.Width * 0.5f, numberBounds.Height * 0.5f);
            number.Position = new Vector2f(30, 4 * numberBounds.Height + 3 * numberBounds.Height * placement);
            target.Draw(number);

            Text[] playerScore = pair.Value;
            Text name = playerScore[0];
            FloatRect nameBounds = playerScore[0].GetGlobalBounds();
            name.Origin = new Vector2f(0, nameBounds.Height * 0.5f);
            name.Position = new Vector2f(92, number.Position.Y);
            
            Text score = playerScore[1];
            FloatRect scoreBounds = playerScore[1].GetGlobalBounds();
            score.Origin = new Vector2f(scoreBounds.Width * 0.5f, scoreBounds.Height * 0.5f);
            score.Position = new Vector2f(424, number.Position.Y);

            target.Draw(name);
            target.Draw(score);
        
        }
        
        for (int i = 0; i <= 2; i++)
        {
            if (i == 0)
            {
                target.Draw(_texts[0]);
                continue;
            }
            Text text = _texts[i];
            FloatRect bounds = text.GetGlobalBounds();
            text.Origin = new Vector2f(bounds.Width * 0.5f, bounds.Height * 0.5f);
            text.Position = new Vector2f(256, 800 + 2f * bounds.Height * i);
            target.Draw(text);
        }
    }
}
