namespace Invaders;

public abstract class Ship : Entity, IHealth, IMoveable, IExplodeable, IDoesDamage, ICanShoot
{
    public int Health { get; set; }
    public int Damage { get; set; }

    protected Ship() : base("test")
    {
        
    }

    public void Move()
    {
        throw new NotImplementedException();
    }

    public void Explode()
    {
        throw new NotImplementedException();
    }

    public void Shoot()
    {
        throw new NotImplementedException();
    }

}