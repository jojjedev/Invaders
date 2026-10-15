using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;


namespace Invaders;

public sealed class PlayerShip : Ship
{
    private float _invulnTimer;
    
    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(Bounds.Width * 0.5f, Bounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2);
        Health = 3;
    }
    
    public override void Move()
    {
        //TODO: När PlayerShip skapas prenumerar man på Events för knapptryck av ASDW/Piltangenter.
        // Om GameOverEventet sker, avslutas prenumerationen så att spelaren kan skriva sitt namn med tangenterna.
        
    }

    private void ChangeDirection(float dt, float x, float y)
    {

        Vector2f newPos = Position; // TODO: Implementera likt Dodgemania
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        scene.Events.LoseHealth += OnLoseHealth;
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= OnLoseHealth;
    }

    private void OnLoseHealth(Scene scene, int amount)
    {
        Invulnerable = true;
        _invulnTimer = 0;
        Sprite.Color = new Color(255, 255, 255, 100);
    }

    public void OnCollision(Scene scene)
    {
        Health--;
        if (Health <= 0)
        {
            Explode();
            scene.Events.PublishGameOver(scene);
            Destroy(scene);
        }
        else scene.Events.PublishLoseHealth(1);
    }
}