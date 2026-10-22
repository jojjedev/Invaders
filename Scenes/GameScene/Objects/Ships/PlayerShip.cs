using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;

namespace Invaders;

public sealed class PlayerShip : Ship
{
    private float _invulnTimer;
    private const int StartHealth = 3;
    private bool _shootPressed;
    private const int PlayerSpeed = 300;
    private const float PlayerShotCd = 0.5f;
    private readonly Color _invulnColor = new Color(100, 100, 100);
    private readonly InputConfig _inputConfig;

    public PlayerShip(InputConfig inputConfig, IntRect playerTexture)
    {
        Sprite.TextureRect = playerTexture;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT * 0.75f);
        Sprite.Rotation = Rotation;
        Health = StartHealth;
        Speed = PlayerSpeed;
        ShotCooldown = PlayerShotCd;
        _inputConfig = inputConfig;
    }

    public override void Create(Scene scene)
    {
        Affiliation = Team.Player;
        
        scene.Events.LoseHealth += OnLoseHealth;
        base.Create(scene);
    }

    public override void Destroy()
    {
        base.Destroy();
        CurrentScene.Events.LoseHealth -= OnLoseHealth;
    }

    private void OnLoseHealth(int amount)
    {
        CurrentScene.Assets.PlaySound("sfx_twoTone", 100, false);
    }

    public override void OnCollision()
    {
        base.OnCollision();
        Health--;
        CurrentScene.Events.PublishLoseHealth(1);
        if (Health <= 0)
        {
            Explode();
            CurrentScene.Events.PublishLoseHealth(1);
            CurrentScene.Events.PublishGameOver();
            Destroy();
        }
        else
        {
            Invulnerable = true;
            _invulnTimer = 0;
        }
    }
    
    private void InvulnCheck()
    {
        Invulnerable = !(_invulnTimer > 1f);
        if (Invulnerable) Sprite.Color = _invulnColor;
        else Sprite.Color = Color.White;
    }

    protected override void Shoot()
    {
        base.Shoot();
        ShootTimer = 0;
        _shootPressed = true;
    }
    private void Move(float dt)
    {
        int XDirection = 0;
        int YDirection = 0;
        if (Keyboard.IsKeyPressed(_inputConfig.Left)) XDirection--;
        if (Keyboard.IsKeyPressed(_inputConfig.Right)) XDirection++;
        if (Keyboard.IsKeyPressed(_inputConfig.Up)) YDirection--;
        if (Keyboard.IsKeyPressed(_inputConfig.Down)) YDirection++;
        if (XDirection != 0 || YDirection != 0)
            ChangeDirection(XDirection, YDirection, dt);
        Rotate();
    }
    private void Rotate()
    {
        if (Keyboard.IsKeyPressed(_inputConfig.RotateLeft)) Rotation -= 0.05f;
        if (Keyboard.IsKeyPressed(_inputConfig.RotateRight)) Rotation += 0.05f;
    }
    private void ChangeDirection(float x, float y, float dt)
    {
        Vector2f oldPos = Position;
        Vector2f velocity = new Vector2f(x, y) / MathF.Sqrt(x*x + y*y) * Speed;
        Vector2f newPos = oldPos + velocity * dt;
        Position = newPos;
        if (IsOutOfBounds()) Position = oldPos;
    }
    public override void Update(float dt)
    {
        base.Update(dt);
        Move(dt);
        if (Keyboard.IsKeyPressed(_inputConfig.Shoot) && (ShootTimer > ShotCooldown || !_shootPressed))
        {
            Shoot();
        }
        if (!Keyboard.IsKeyPressed(_inputConfig.Shoot)) _shootPressed = false;
        if (Invulnerable) InvulnCheck();
        
        _invulnTimer += dt;
        ShootTimer += dt;
        
    }

}