using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Ship : Entity, IMoveable, IExplodeable, ICanShoot, IDamageable, IInteractable
{
    private Vector2f _spawnPosition;
    private float _originalSpeed;
    protected float Speed;
    protected Vector2f Direction;
    // Constructor
    protected Ship() : base("sheet") { }
    
    // Properties
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsDestroyed { get; set; }
    public bool Invulnerable { get; set; }
    protected Team Affiliation { get; set; }
    public virtual Vector2f Position
    {
        get => Sprite.Position;
        set => Sprite.Position = value;
    }
    public virtual FloatRect Bounds => Sprite.GetGlobalBounds();

    // Methods
    public override void Create(Scene scene)
    {
        base.Create(scene);
        if (!scene.InteractableEntities.ContainsKey(Affiliation))
        {
            scene.InteractableEntities.Add(Affiliation, new List<IInteractable>());
        }
        scene.InteractableEntities[Affiliation].Add(this);
        _spawnPosition = Position;
        _originalSpeed = Speed;
    }

    public virtual void Move(float dt) { }

    

    public virtual void Shoot()
    {
        //TODO Skapa två instanser av Bullet ifrån skeppets position. Insp från Dodgemania
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