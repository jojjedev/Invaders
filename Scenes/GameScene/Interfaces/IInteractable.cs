using SFML.Graphics;

namespace Invaders;

public interface IInteractable
{
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
    public bool ToBeDestroyed { get; set; }
    public FloatRect CollisionBounds { get;}
    public void OnCollision(){}
}

public enum Team
{
    Player,
    Enemy
}