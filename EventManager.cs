using System.Runtime.CompilerServices;

namespace Invaders;

public delegate void ValueChangedEvent(Scene scene, int amount);
public sealed class EventManager
{
    public event ValueChangedEvent LoseHealth;
    private int _healthLost;


    public void PublishLoseHealth(int amount) => _healthLost += amount;


    public void CheckEvent(Scene scene)
    {
        if (_healthLost != 0)
        {
            LoseHealth?.Invoke(scene, _healthLost);
            _healthLost = 0;
        }
    }
}