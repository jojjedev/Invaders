namespace Invaders;

public interface ICanShoot
{
    public void Shoot();
    public void DealDamage(IDamageable other);
}