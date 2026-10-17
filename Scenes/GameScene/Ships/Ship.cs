using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public abstract class Ship : Entity, IMoveable, IExplodeable, ICanShoot, IDamageable, IInteractable
{
    private Vector2f _spawnPosition;
    private float _originalSpeed;
    protected Vector2f Direction;
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
        _spawnPosition = Position;
        _originalSpeed = Speed;
    }

    protected override void OnCollision(Scene scene)
    {
        base.OnCollision(scene);
        
    }

    public virtual void Move(float dt) { }

    public virtual bool IsOutOfBounds()
    {
        return
            GlobalBounds.Intersects(new FloatRect(SCREEN_WIDTH, 0, 1, SCREEN_HEIGHT)) ||
            GlobalBounds.Intersects(new FloatRect(-1, 0, 1, SCREEN_HEIGHT)) ||
            GlobalBounds.Intersects(new FloatRect(0, SCREEN_HEIGHT, 1, SCREEN_WIDTH)) ||
            GlobalBounds.Intersects(new FloatRect(0, -1, 1, SCREEN_WIDTH));
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