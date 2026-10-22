using System.Reflection.Metadata.Ecma335;
using System.Text;
using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static Invaders.Constants;
using System.IO;

namespace Invaders;

public class GameGUI : GUI
{
    private int _thresholdTracker;
    private const int Threshold = 250;
    private float _scoreTimer;
    private const float ScoreInterval = 0.1f;
    private float _colorTimer;
    private bool _gameOver, _loadScene;
    private string _inputName = "";
    private readonly GameScene _currentScene;
    public GameGUI(GameScene scene)
    {
        _currentScene = scene;
        Texts = new List<Text>();
        DefaultCharacterSize = 20;
        DefaultColor = Color.White;
        CreateObjects();
        _currentScene.Events.IncreaseScore += OnScoreIncrease;
        _currentScene.Events.GameOver += CreateEndScene;
        _currentScene.Events.NewInput += PlayerInput;
    }
    private int Score { get; set; }
    public override void Destroy()
    {
        base.Destroy();
        _currentScene.Events.IncreaseScore -= OnScoreIncrease;
    }
    private void CreateObjects()
    {
        if (_currentScene.FindByType(out PlayerShip ship))
        {
            Console.WriteLine("Found");
            Color color = DefaultColor;
            Texts.Add(CreateText(_currentScene,$"Score: {Score}", DefaultCharacterSize, color));
            Texts.Add(CreateText(_currentScene, $"Health: {ship.Health}", DefaultCharacterSize, color));
            SetTextsPosition();
        };

    }
    private void PassiveScoreIncrease()
    {
        Score++;
        _thresholdTracker++;
    }
    private void OnScoreIncrease(int amount)
    {
        Score += amount;
        _thresholdTracker += amount;
        Texts[0].FillColor = Color.Green;
        _colorTimer = 0.5f;
    }
    private void SetTextsPosition()
    {
        Text scoreText = Texts[0];
        FloatRect scoreBounds = scoreText.GetGlobalBounds();
        scoreText.Position = new Vector2f(scoreBounds.Height, scoreBounds.Height);

        Text healthText = Texts[1];
        FloatRect healthBounds = Texts[1].GetGlobalBounds();
        healthText.Position = new Vector2f(SCREEN_WIDTH - healthBounds.Width - scoreBounds.Height, scoreBounds.Height);

    }
    private bool IsThresholdPassed()
    {
        if (_thresholdTracker >= Threshold)
        {
            _thresholdTracker -= Threshold;
            return true;
        }
        return false;
    }

    private void CreateEndScene()
    {
        _gameOver = true;
        Texts.Clear();
        Text gameOverText = CreateText(_currentScene, $"Game over!\nYour score is\n{Score}\nEnter your name", 45, DefaultColor);
        Texts.Add(gameOverText);
        gameOverText.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        gameOverText.Origin = new Vector2f(gameOverText.GetLocalBounds().Width / 2f,
            gameOverText.GetLocalBounds().Height / 2f);
        
        Text inputText = CreateText(_currentScene,_inputName, 45, DefaultColor);
        Texts.Add(inputText);
        inputText.Position = new Vector2f(SCREEN_WIDTH / 2f - 6.5f * 25, 800);
        inputText.Origin = new Vector2f(0, inputText.GetLocalBounds().Height / 2f);
    }
    
    private void PlayerInput(string input)
    {
        if (input == "\b")
        {
            if (_inputName.Length != 0) _inputName = _inputName.Substring(0, _inputName.Length - 1);
        }
        else if (_inputName.Length <= 12 && input != ",")
        {
            _inputName += input;
        }
    }

    private void SaveScore()
    {
        string currentDir = Directory.GetCurrentDirectory();

        string fileMain = "HighScore/highscore.txt";
        string fileIfDebug = "../../../HighScore/highscore.txt";

        if (currentDir.Substring(currentDir.Length - 7, 7) == "net10.0")
        { 
            fileMain = "../../../HighScore/highscore.txt";
        }

        string fileContents = "";
        if (File.Exists(fileMain)) fileContents = File.ReadAllText(fileMain);
        fileContents += $"{_inputName.Trim()},{Score}\n";
        File.WriteAllText(fileMain, fileContents, Encoding.UTF8);
    }
    public override void Update(float dt)
    {
        base.Update(dt);
        if (!_gameOver)
        {
            Texts[0].DisplayedString = $"Score {Score}";
            _scoreTimer += dt;
            if (_scoreTimer > ScoreInterval)
            {
                PassiveScoreIncrease();
                _scoreTimer = 0;
            }

            _colorTimer -= dt;
            if (_colorTimer <= 0)
            {
                Texts[0].FillColor = Color.White;
                _colorTimer = 0;
            }

            if (IsThresholdPassed()) _currentScene.Events.PublishThresholdEvent();

            if (_currentScene.FindByType(out PlayerShip ship))
            {
                Texts[1].DisplayedString = $"Health: {ship.Health}";

            }
            else Texts[1].DisplayedString = $"Health: 0";
        }
        else
        {
            _currentScene.Events.CheckInputEvent();
            Texts[1].DisplayedString = _inputName;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
            {
                SaveScore();
                _loadScene = true;
            }
            if (_loadScene) _currentScene.Events.PublishSceneChange(SceneName.Scoreboard);
        }
    }

    public override void Render(RenderTarget target)
    {
        foreach (Text text in Texts)
        {
            target.Draw(text);
        }
    }
}