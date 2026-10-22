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
    private readonly PlayerShipHandler _handler;
    private const int StartHealth = 3;
    private bool _spacePressed;
    private const int PlayerSpeed = 300;
    private const float PlayerShotCd = 0.5f;
    private readonly Color InvulnColor = new Color(100, 100, 100);

    public PlayerShip()
    {
        Sprite.TextureRect = BluePlayerShipRect;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = new Vector2f(SCREEN_WIDTH / 2f, SCREEN_HEIGHT * 0.75f);
        Sprite.Rotation = Rotation;
        Health = StartHealth;
        Speed = PlayerSpeed;
        ShotCooldown = PlayerShotCd;
        _handler = new PlayerShipHandler();
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
        if (Invulnerable) Sprite.Color = InvulnColor;
        else Sprite.Color = Color.White;
    }

    protected override void Shoot()
    {
        base.Shoot();
        ShootTimer = 0;
        _spacePressed = true;
    }

    public override void Update(float dt)
    {
        base.Update(dt);
        _handler.Move(this, dt);
        if (Keyboard.IsKeyPressed(Space) && (ShootTimer > ShotCooldown || !_spacePressed))
        {
            Shoot();
        }
        if (!Keyboard.IsKeyPressed(Space)) _spacePressed = false;
        if (Invulnerable) InvulnCheck();
        
        _invulnTimer += dt;
        ShootTimer += dt;
        
    }

}