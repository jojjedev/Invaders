using System.Runtime.CompilerServices;

namespace Invaders;

public delegate void ChangeSceneEvent(SceneName sceneName);
public delegate void ValueChangedEvent(Scene scene, int amount);
public delegate void GameOverEvent(Scene scene);
public sealed class EventManager
{
    private int _healthLost = 0;
    private SceneName _newScene = SceneName.Waiting;
    private bool _gameOver;
    public event ValueChangedEvent LoseHealth;
    public event ChangeSceneEvent SceneChange;
    public event GameOverEvent GameOver;
    public void PublishLoseHealth(int amount) => _healthLost += amount;
    public void PublishSceneChange(SceneName sceneName) => _newScene = sceneName;
    public void PublishGameOver(Scene scene) => _gameOver = true;

    public void CheckLoseHealthEvent(Scene scene)
    {
        if (_healthLost != 0)
        {
            LoseHealth?.Invoke(scene, _healthLost);
            _healthLost = 0;
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

    public void CheckGameOverEvent(Scene scene)
    {
        if (_gameOver)
        {
            GameOver?.Invoke(scene);
            _gameOver = false;
        }
    }
}