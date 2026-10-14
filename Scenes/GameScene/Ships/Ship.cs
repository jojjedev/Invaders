namespace Invaders;

public abstract class Ship : Entity, IMoveable, IExplodeable, ICanShoot, IDamageable
{
    protected Ship() : base("sheet")
    {
        
    }
    public int Health { get; set; }
    public int Damage { get; set; }
    public bool IsDestroyed { get; set; }

    public virtual void Move()
    {
        //TODO: Generell funktion för att uppdatera position som kan ärvas till barnklasserna
    }

    public virtual void Explode()
    {
        //TODO Ladda in Sprite för explosion på skeppets position
    }

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
}