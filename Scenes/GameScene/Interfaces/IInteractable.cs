using SFML.Graphics;

namespace Invaders;

public interface IInteractable
{
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
}

public enum Team
{
    Player,
    Enemy
}