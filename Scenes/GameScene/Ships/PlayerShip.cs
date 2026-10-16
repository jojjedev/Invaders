using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;

namespace Invaders;

public sealed class PlayerShip : Ship
{
    private float _invulnTimer;

    private int _startHealth;
    // Debug
    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(Bounds.Width * 0.5f, Bounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        Sprite.Rotation = Rotation;
        _startHealth = 3;
        Health = 3;
    }
    
    public override void Create(Scene scene)
    {
        
        Affiliation = Team.Player;
        Health = _startHealth;
        Speed = 400;
        scene.Events.LoseHealth += OnLoseHealth;
        base.Create(scene);
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= OnLoseHealth;
    }
    
    // EVENTS
    private void OnLoseHealth(Scene scene, int amount)
    {
        Invulnerable = true;
        _invulnTimer = 0;
        Sprite.Color = new Color(255, 255, 255, 100);
    }

    protected override void OnCollision(Scene scene)
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

    // MOVEMENT
    private void Rotate()
    {
        if (Keyboard.IsKeyPressed(Left)) Rotation -= 2;
        if (Keyboard.IsKeyPressed(Right)) Rotation += 2;
        Sprite.Rotation = Rotation;
    }
    public override void Move(float dt)
    {
        int x = 0;
        int y = 0;
        if (Keyboard.IsKeyPressed(A)) x--;
        if (Keyboard.IsKeyPressed(D)) x++;
        if (Keyboard.IsKeyPressed(W)) y--;
        if (Keyboard.IsKeyPressed(S)) y++;
        Rotate();
        if (x != 0 || y != 0) ChangeDirection(x,y,dt);
        if (!IsOutOfBounds()) ;
    }
    private void ChangeDirection(float x, float y, float dt)
    {
        Vector2f newPos = Position; // TODO: Implementera likt Dodgemania
        Vector2f velocity = new Vector2f(x, y) / MathF.Sqrt(x*x + y*y) * Speed;
        newPos += velocity * dt;
        Position = newPos;
    }


    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        Move(dt);
        if (Keyboard.IsKeyPressed(Space)) Shoot(scene, this);
        
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
    }
}