namespace Invaders;

public interface ICanShoot
{
    public void Shoot(Scene scene, Ship ship);
    public void DealDamage(IDamageable other);
}