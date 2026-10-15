using SFML.Graphics;

namespace Invaders;

public interface IInteractable
{
    public bool Invulnerable { get; set; }
    public FloatRect Bounds { get;}
}

public enum Team
{
    Player,
    Enemy
}