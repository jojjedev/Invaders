using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static Invaders.Constants;
using static SFML.Window.Keyboard.Key;

namespace Invaders;

public sealed class PlayerShip : Ship
{
    private float _invulnTimer;
    private readonly PlayerShipHandler _handler;
    private const int StartHealth = 3;
    private bool _spacePressed;
    // Debug

    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        Sprite.Rotation = Rotation;
        Health = StartHealth;
        Speed = 400;
        ShotCooldown = 0.5f;
        _handler = new PlayerShipHandler();
    }

    public override void Create(Scene scene)
    {
        Affiliation = Team.Player;
        
        scene.Events.LoseHealth += OnLoseHealth;
        base.Create(scene);
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
        scene.Events.LoseHealth -= OnLoseHealth;
    }

    private void OnLoseHealth(int amount)
    {
    }

    public override void OnCollision()
    {
        base.OnCollision();
        Health--;
        CurrentScene.Events.PublishLoseHealth(1);
        if (Health <= 0)
        {
            Explode(CurrentScene);
            CurrentScene.Events.PublishLoseHealth(1);
            CurrentScene.Events.PublishGameOver(CurrentScene);
            Destroy(CurrentScene);
        }
        else
        {
            Invulnerable = true;
            _invulnTimer = 0;
        }
        Console.WriteLine(Health);
    }
    

    private void InvulnCheck()
    {
        Invulnerable = !(_invulnTimer > 1f);
        if (Invulnerable) Sprite.Color = new Color(100, 100, 100);
        else Sprite.Color = new Color(255, 255, 255);
    }

    public override void Shoot(Scene scene, Ship ship)
    {
        base.Shoot(scene, ship);
        ShootTimer = 0;
        _spacePressed = true;
    }

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        _handler.Move(this, dt);
        if (Keyboard.IsKeyPressed(Space) && (ShootTimer > ShotCooldown || !_spacePressed))
        {
            Shoot(scene, this);
        }
        if (!Keyboard.IsKeyPressed(Space)) _spacePressed = false;
        if (Invulnerable) InvulnCheck();
        
        _invulnTimer += dt;
        ShootTimer += dt;
        
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
    }
}