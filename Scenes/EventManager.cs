using System.Runtime.CompilerServices;

namespace Invaders;

public delegate void ChangeSceneEvent(SceneName sceneName);
public delegate void ValueChangedEvent(Scene scene, int amount);
public sealed class EventManager
{
    private int _healthLost = 0;
    private SceneName _newScene = SceneName.Waiting;
    public event ValueChangedEvent LoseHealth;
    public event ChangeSceneEvent SceneChange;

    public void PublishLoseHealth(int amount) => _healthLost += amount;
    public void PublishSceneChange(SceneName sceneName) => _newScene = sceneName;

    public void CheckEvent(Scene scene)
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
}