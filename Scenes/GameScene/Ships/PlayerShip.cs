using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;

namespace Invaders;

public sealed class PlayerShip : Ship
{
    private int _degreeRotation = 0;
    private float _invulnTimer;
    
    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(Bounds.Width * 0.5f, Bounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        Sprite.Rotation = _degreeRotation;
    }
    private void Rotate()
    {
        if (Keyboard.IsKeyPressed(Left)) _degreeRotation -= 2;
        if (Keyboard.IsKeyPressed(Right)) _degreeRotation += 2;
        Sprite.Rotation = _degreeRotation;

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
    }
    private void ChangeDirection(float x, float y, float dt)
    {
        Vector2f newPos = Position; // TODO: Implementera likt Dodgemania
        Vector2f velocity = new Vector2f(x, y) / MathF.Sqrt(x*x + y*y) * Speed;
        newPos += velocity * dt;
        Position = newPos;
    }
    public override void Create(Scene scene)
    {
        base.Create(scene);
        Affiliation = Team.Player;
        Health = 3;
        Speed = 400;
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

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        Move(dt);
    }

    
}