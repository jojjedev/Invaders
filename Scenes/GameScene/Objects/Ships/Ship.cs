using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public abstract class Ship : Entity, IInteractable
{
    // Constructor
    protected Ship() : base("sheet") { }
    
    // Properties
    public int Health { get; protected set; }
    protected float Speed { get; set; }
    public Team Affiliation { get; set; }
    public bool Invulnerable { get; set; }
    protected float ShotCooldown { get; set; }
    protected float ShootTimer { get; set; }

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

    public override void Destroy()
    {
        ToBeDestroyed = true;
    }
    public virtual void OnCollision() {}
    public virtual bool IsOutOfBounds()
    {
        return Position.X + LocalBounds.Width / 2f > SCREEN_WIDTH ||
               Position.X - LocalBounds.Width / 2f < 0 ||
               Position.Y + LocalBounds.Width / 2f > SCREEN_HEIGHT ||
               Position.Y - LocalBounds.Width / 2f < 0;
    }
    protected virtual void Shoot()
    {
        CurrentScene.Spawn(new Bullet(this, Side.Left));
        CurrentScene.Spawn(new Bullet(this, Side.Right));
    }
    public virtual void Move(float dt) { }
    protected virtual void Explode()
    {
        CurrentScene.Spawn(new Explosion(this));
        CurrentScene.Assets.PlaySound("explosionCrunch_000", 25, false);
    }
    
}