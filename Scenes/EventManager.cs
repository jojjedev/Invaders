using System.Runtime.CompilerServices;

namespace Invaders;

public delegate void ChangeSceneEvent(SceneName sceneName);
public delegate void ValueChangedEvent(Scene scene, int amount);
public sealed class EventManager
{
    private int _healthLost;
    private int _sceneChange;
    public event ValueChangedEvent LoseHealth;
    public event ChangeSceneEvent SceneChange;

    public void PublishLoseHealth(int amount) => _healthLost += amount;
    public void PublishSceneChange(SceneName sceneName) => _sceneChange++;

    /*public void CheckEvent(Scene scene)
    {
        if (_healthLost != 0)
        {
            LoseHealth?.Invoke(scene, _healthLost);
            _healthLost = 0;
        }
        
    }*/

    public void CheckSceneNameEvent(SceneName sceneName)
    {
        if (_sceneChange != 0)
        {
            SceneChange?.Invoke(sceneName);
            _sceneChange = 0;
        }
    }
}