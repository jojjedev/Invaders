using System.Runtime.CompilerServices;
using SFML.Window;

namespace Invaders;

public delegate void ChangeSceneEvent(SceneName sceneName);
public delegate void ValueChangedEvent(int amount);

public delegate void InputEvent(string input);
public delegate void ScoreThresholdEvent();
public delegate void GameOverEvent(Scene scene);
public sealed class EventManager
{
    private int _healthLost = 0;
    private int _scoreIncrease = 0;
    private SceneName _newScene = SceneName.Waiting;
    private bool _thresholdPassed;
    private bool _gameOver;
    private string _input;
    public event ValueChangedEvent LoseHealth;
    public event ValueChangedEvent IncreaseScore;
    public event ChangeSceneEvent SceneChange;
    public event ScoreThresholdEvent ThresholdPassed;
    public event GameOverEvent GameOver;
    public event InputEvent NewInput;
    public void PublishLoseHealth(int amount) => _healthLost += amount;
    public void PublishSceneChange(SceneName sceneName) => _newScene = sceneName;
    public void PublishThresholdEvent() => _thresholdPassed = true;
    public void PublishGameOver(Scene scene) => _gameOver = true;
    public void PublishScoreIncrease(int amount) => _scoreIncrease += amount;
    public void PublishInputEvent(string input) => _input = input;
    
    
    public void CheckValueChangeEvent()
    {
        if (_healthLost != 0)
        {
            LoseHealth?.Invoke(_healthLost);
            _healthLost = 0;
        }

        if (_scoreIncrease != 0)
        {
            IncreaseScore?.Invoke(_scoreIncrease);
            _scoreIncrease = 0;
        }
        
    }
    public void CheckSceneNameEvent()
    {
        if (_newScene != SceneName.Waiting)
        {
            SceneChange?.Invoke(_newScene);
            _newScene = SceneName.Waiting;
        }
    }

    public void CheckThresholdEvent()
    {
        if (_thresholdPassed)
        {
            ThresholdPassed?.Invoke();
            _thresholdPassed = false;
        }
    }

    public void CheckGameOverEvent(Scene scene)
    {
        if (_gameOver)
        {
            GameOver?.Invoke(scene);
            _gameOver = false;
        }
    }

    public void CheckInputEvent()
    {
        if (_input != "")
        {
            NewInput?.Invoke(_input);
            _input = "";
        }
    }
}