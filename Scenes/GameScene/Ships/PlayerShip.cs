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
    private readonly int _startHealth;
    private bool _spacePressed;
    // Debug

    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT / 2f);
        Sprite.Rotation = Rotation;
        _startHealth = 3;
        Health = 3;
        ShotCooldown = 0.5f;
        _handler = new PlayerShipHandler();
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

    private void OnLoseHealth(Scene scene,int amount)
    {
        Invulnerable = true;
        _invulnTimer = 0;
    }

    protected override void OnCollision(Scene scene)
    {
        if (Invulnerable) return;
        Health--;
        if (Health <= 0)
        {
            Explode();
            scene.Events.PublishGameOver(scene);
            Destroy(scene);
        }
        else scene.Events.PublishLoseHealth(1);
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
        if (Keyboard.IsKeyPressed(I)) OnCollision(scene);
    }

    public override void Render(RenderTarget target)
    {
        base.Render(target);
    }
}