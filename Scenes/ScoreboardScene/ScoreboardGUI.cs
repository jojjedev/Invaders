using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public sealed class ScoreboardGUI : GUI
{
    private readonly Dictionary<Text, Text[]> _scores;
    private readonly Color _scoreColor;
    private readonly ScoreboardScene _currentScene;
    public ScoreboardGUI(ScoreboardScene scene) : base()
    {
        _currentScene = scene;
        Texts = new List<Text>();
        _scores = new Dictionary<Text, Text[]>();
        DefaultCharacterSize = 32;
        _scoreColor = new Color(168, 154, 226);
        CreateAll();
    }
    private void CreateAll()
    {
        CreateTitleAndBack();
        CreateScoreboard();
    }
    private void CreateTitleAndBack()
    {
        Text title = CreateText(_currentScene, "Scoreboard", 50, Color.Cyan);
        FloatRect titleBounds = title.GetGlobalBounds();
        title.Origin = new Vector2f(titleBounds.Width * 0.5f, titleBounds.Height * 0.5f);
        title.Position = new Vector2f(256, titleBounds.Height * 2f);
        Texts.Add(title);
        Texts.Add(CreateText(_currentScene, "New Game", 40, DefaultColor)); // Index 1
        Texts.Add(CreateText(_currentScene, "Menu", 40, DefaultColor)); // Index 2
        CurrentTarget = 1; // Sätter target på "New Game" by default.
        Target();
    }
    private void CreateScoreboard()
    {
        Text[] placements = new Text[10];
        List<Text[]> storedScores = StoreHighScore();
        for (int i = 0; i <= placements.Length -1; i++)
        {
            placements[i] = CreateText(_currentScene, $"{i + 1}", DefaultCharacterSize, _scoreColor);
            if (!(i > storedScores.Count - 1))
            {
                _scores[placements[i]] = storedScores[i];
            }
        }
        
    }

    public override int Target()
    {
        Text target = Texts[CurrentTarget];
        target.CharacterSize = 45;
        target.FillColor = Color.White;
        return Texts.IndexOf(target);
    }
    public override void UpdateTarget()
    {
        if (Keyboard.IsKeyPressed(Down) || 
            Keyboard.IsKeyPressed(S) || 
            Keyboard.IsKeyPressed(Up) || 
            Keyboard.IsKeyPressed(W))
        {
            Texts[CurrentTarget].CharacterSize = 40;
            Texts[CurrentTarget].FillColor = DefaultColor;
            CurrentTarget = CurrentTarget == 1 ? CurrentTarget = 2 : CurrentTarget = 1;
            Target();
            _currentScene.Assets.PlaySound("select_002", 25, false);
        }
        
    }
    
    private List<Text[]> StoreHighScore()
    {
        string currentDir = Directory.GetCurrentDirectory();

        string fileMain = "HighScore/highscore.txt";
        string fileIfDebug = "../../../HighScore/highscore.txt";

        if (currentDir.Substring(currentDir.Length - 7, 7) == "net10.0")
        { 
            fileMain = "../../../HighScore/highscore.txt";
        }

        string[] fileContents = new string[0];
        if (File.Exists(fileMain)) fileContents = File.ReadAllLines(fileMain);
        
        List<Text[]> playerScores = new List<Text[]>();

        foreach (string line in fileContents)
        {
            string[] splitLine = line.Split(",");
            string name = splitLine[0];
            string score = splitLine[1];
            Text[] playerScore = new Text[]
            {
                CreateText(_currentScene, name, DefaultCharacterSize, _scoreColor),             
                CreateText(_currentScene, score, DefaultCharacterSize, _scoreColor)
            };
            playerScores.Add(playerScore);
        }

        playerScores = playerScores.OrderByDescending(e => GetScore(e)).ToList();
        
        return playerScores;
    }
    private int GetScore(Text[] entry)
    {
        int num = int.Parse(entry[1].DisplayedString);
        return num;
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
            number.Position = new Vector2f(0.06f * SCREEN_WIDTH, 4 * numberBounds.Height + 3 * numberBounds.Height * placement);
            target.Draw(number);

            Text[] playerScore = pair.Value;
            Text name = playerScore[0];
            FloatRect nameBounds = playerScore[0].GetGlobalBounds();
            name.Origin = new Vector2f(0, nameBounds.Height * 0.5f);
            name.Position = new Vector2f(0.18f * SCREEN_WIDTH, number.Position.Y);
            
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
                target.Draw(Texts[0]);
                continue;
            }
            Text text = Texts[i];
            FloatRect bounds = text.GetGlobalBounds();
            text.Origin = new Vector2f(bounds.Width * 0.5f, bounds.Height * 0.5f);
            text.Position = new Vector2f(256, SCREEN_HEIGHT - bounds.Height * 4.5f + 1.5f * bounds.Height * i);
            target.Draw(text);
        }
    }
}
