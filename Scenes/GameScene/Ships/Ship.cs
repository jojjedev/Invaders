using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public abstract class Ship : Entity, IMoveable, IExplodeable, ICanShoot, IDamageable, IInteractable
{
    // Constructor
    protected Ship() : base("sheet") { }
    
    // Properties
    public int Health { get; set; }
    public int Damage { get; set; }
    public float Speed { get; set; }
    protected float ShotCooldown { get; set; }
    protected float ShootTimer { get; set; }
    public bool IsDestroyed { get; set; }
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
    // Methods
    public override void Create(Scene scene)
    {
        base.Create(scene);
        if (!scene.InteractableEntities.ContainsKey(Affiliation))
        {
            scene.InteractableEntities.Add(Affiliation, new List<Entity>());
        }
        scene.InteractableEntities[Affiliation].Add(this);
    }
    public virtual void Move(float dt) { }

    public virtual bool IsOutOfBounds()
    {
        return Position.X + LocalBounds.Width / 2f > SCREEN_WIDTH ||
               Position.X - LocalBounds.Width / 2f < 0 ||
               Position.Y + LocalBounds.Width / 2f > SCREEN_HEIGHT ||
               Position.Y - LocalBounds.Width / 2f < 0;
    }

    public virtual void Shoot(Scene scene, Ship ship)
    {
        Bullet leftShot = new Bullet(ship, Side.Left);
        Bullet rightShot = new Bullet(ship, Side.Right);
        scene.Spawn(leftShot);
        scene.Spawn(rightShot);
        ShootTimer = 0; 
    }
    
    public virtual void DealDamage(IDamageable other)
    {
        //TODO Ifall träff, kalla på denna. Tar bort HP från motståndare
    }


    public virtual void TakeDamage(int amount)
    {
        //TODO Minska HP, om <= 0. Kalla på slutgiltig funktion och Destroy().
    }

    public virtual void Explode()
    {
        //TODO Ladda in Sprite för explosion på skeppets position
    }
}