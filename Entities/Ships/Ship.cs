namespace Invaders;

public abstract class Ship : Entity, IMoveable, IExplodeable, ICanShoot, IDamageable
{
    protected Ship() : base("test")
    {
        
    }
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsDestroyed { get; set; }

    public virtual void Move()
    {
        throw new NotImplementedException();
    }

    public virtual void Explode()
    {
        throw new NotImplementedException();
    }

    public virtual void Shoot()
    {
        throw new NotImplementedException();
    }

    public virtual void DealDamage(IDamageable other)
    {
        throw new NotImplementedException();
    }

    public virtual void TakeDamage(int amount)
    {
        throw new NotImplementedException();
    }
}