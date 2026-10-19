namespace Invaders;

public interface IDamageable
{
    public bool IsDestroyed { get; set; }
    public void TakeDamage(int amount);

}