using SFML.System;
using SFML.Graphics;
using SFML.Window;
using static Invaders.Constants;


namespace Invaders;

public class GameGUI : GUI
{
    private int _thresholdTracker;
    private int _threshold = 250;
    private float ScoreTimer;
    private const float ScoreInterval = 0.1f;
    private float _colorTimer;
    private bool _gameOver;
    private string _inputName;
    private char[] _charArray = new char[] { '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_', '_' };
    public GameGUI(GameScene scene)
    {
        Texts = new List<Text>();
        DefaultCharacterSize = 20;
        DefaultColor = Color.White;
        CreateObjects(scene);
        scene.Events.IncreaseScore += OnScoreIncrease;
        scene.Events.GameOver += CreateEndScene;
        scene.Events.NewInput += PlayerInput;
    }
    private int Score { get; set; }
    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.IncreaseScore -= OnScoreIncrease;
    }
    private void CreateObjects(GameScene scene)
    {
        if (scene.FindByType(out PlayerShip ship))
        {
            Console.WriteLine("Found");
            Color color = DefaultColor;
            Texts.Add(CreateText(scene,$"Score: {Score}", DefaultCharacterSize, color));
            Texts.Add(CreateText(scene, $"Health: {ship.Health}", DefaultCharacterSize, color));
            SetTextsPosition();
        };

    }
    private void PassiveScoreIncrease()
    {
        Score++;
        _thresholdTracker++;
    }
    private void OnScoreIncrease(Scene scene, int amount)
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
        if (_thresholdTracker >= _threshold)
        {
            _thresholdTracker -= _threshold;
            return true;
        }

        return false;
    }

    private void CreateEndScene(Scene scene)
    {
        _gameOver = true;
        Texts.Clear();
        Text gameOverText = CreateText(scene, $"Game over!\nYour score is\n{Score}", 45, DefaultColor);
        Texts.Add(gameOverText);
        gameOverText.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        gameOverText.Origin = new Vector2f(gameOverText.GetLocalBounds().Width / 2f,
            gameOverText.GetLocalBounds().Height / 2f);
        
        Text inputText = CreateText(scene,_inputName, 45, DefaultColor);
        Texts.Add(inputText);
        inputText.Position = new Vector2f(50, 800);
        inputText.Origin = new Vector2f(inputText.GetLocalBounds().Width / 2f,
            inputText.GetLocalBounds().Height / 2f);
    }

    private void PlayerInput(string input)
    {
        if (char.TryParse(input, out char newInput))
        {
            for (int i = 0; i < _charArray.Length; i++)
            {
                if (_charArray[i] == '_' && newInput != '\b')
                {
                    _charArray[i] = newInput;
                    _inputName = new string(_charArray);
                    return;
                }

                if (newInput == '\b')
                {

                    if (_charArray[i] == '_' && i != 0)
                    {
                        _charArray[i - 1] = '_';
                        _inputName = new string(_charArray);
                        return;
                    }

                    if (i == _charArray.Length - 1)
                    {
                        _charArray[i] = '_';
                        _inputName = new string(_charArray);
                        return;
                    }
                }
            }
        }
        else _inputName = new string(_charArray);
    }

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        if (!_gameOver)
        {
            Texts[0].DisplayedString = $"Score {Score}";
            ScoreTimer += dt;
            if (ScoreTimer > ScoreInterval)
            {
                PassiveScoreIncrease();
                ScoreTimer = 0;
            }

            _colorTimer -= dt;
            if (_colorTimer <= 0)
            {
                Texts[0].FillColor = Color.White;
                _colorTimer = 0;
            }

            if (IsThresholdPassed()) scene.Events.PublishThresholdEvent();

            if (scene.FindByType(out PlayerShip ship))
            {
                Texts[1].DisplayedString = $"Health: {ship.Health}";

            }
            else Texts[1].DisplayedString = $"Health: 0";
        }
        else
        {
            scene.Events.CheckInputEvent();
            Texts[1].DisplayedString = _inputName;
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