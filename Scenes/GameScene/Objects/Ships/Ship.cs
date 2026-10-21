using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public abstract class Ship : Entity, IMoveable, IInteractable
{
    // Constructor
    protected Ship() : base("sheet") { }
    
    // Properties
    public int Health { get; protected set; }
    public float Speed { get; set; }
    protected float ShotCooldown { get; set; }
    protected float ShootTimer { get; set; }
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
    public bool HasAlreadyCollided { get; set; }

    public FloatRect CollisionBounds =>
        new FloatRect(Sprite.Position - Sprite.Origin,
            new Vector2f(LocalBounds.Width, LocalBounds.Height)
        );

    public override FloatRect DebugBounds => CollisionBounds;

    public bool ToBeDestroyed
    {
        get => Dead;
        set => Dead = value;
    }

    // Methods

    public override void Destroy(Scene scene)
    {
        ToBeDestroyed = true;
    }

    public virtual void OnCollision()
    {
        if (HasAlreadyCollided) return;
        HasAlreadyCollided = true;
    }

    public virtual bool IsOutOfBounds()
    {
        return Position.X + LocalBounds.Width / 2f > SCREEN_WIDTH ||
               Position.X - LocalBounds.Width / 2f < 0 ||
               Position.Y + LocalBounds.Width / 2f > SCREEN_HEIGHT ||
               Position.Y - LocalBounds.Width / 2f < 0;
    }

    public virtual void Shoot(Scene scene, Ship ship)
    {
        scene.Spawn(new Bullet(ship, Side.Left));
        scene.Spawn(new Bullet(ship, Side.Right));
    }

    public virtual void Move(float dt) { }
    

    public virtual void Explode(Scene scene)
    {
        scene.Spawn(new Explosion(this));
    }

    public override void Update(Scene scene, float dt)
    {
        HasAlreadyCollided = false;
        base.Update(scene, dt);
    }
}