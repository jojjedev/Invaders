using System.Reflection;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public sealed class EnemyShip : Ship
{
    private Vector2f _direction;
    private float _fireChance;
    public EnemyShip(IntRect textureRect, Vector2f pos, int hp, float speed, float shotCd, float fireChance)
    {
        Sprite.TextureRect = textureRect;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = pos;
        _direction = RandomDirection();
        Sprite.Rotation = Rotation;
        Health = hp;
        Speed = speed;
        ShotCooldown = shotCd;
        _fireChance = fireChance;
    }

    public override void Move(float dt)
    {
        Vector2f oldPos = Position;
        Vector2f velocity = _direction * Speed / MathF.Sqrt(2);
        Vector2f newPos = oldPos + velocity * dt;
        Position = newPos;
        if (ShouldReflect())
        {
            Position = oldPos;
            Reflect();
        }

        if (IsOutOfBounds()) Position = new Vector2f(newPos.X, -LocalBounds.Width / 2f);
    }

    public override void Create(Scene scene)
    {
        Affiliation = Team.Enemy;
        base.Create(scene);
    }

    private void Reflect()
    {
        if (_direction.X == 1)
        {
            _direction.X = -1;
            Rotation += 90;
        }
        else
        {
            _direction.X = 1;
            Rotation -= 90;
        }
    }

    //TODO: Om tid över, kolla på att fixa random direction och isåfall också fixa reflect så det passar.
    private Vector2f RandomDirection()
    {
        Random random = new Random();
        int dir = random.Next(0, 2);
        if (dir == 0)
        {
            dir = -1;
            Rotation = 45;
        }
        else
        {
            Rotation = -45;
        }
        return new Vector2f(dir, 1);
    }

    private bool ShouldReflect()
    {
        return Position.X + LocalBounds.Width / 2f > SCREEN_WIDTH ||
               Position.X - LocalBounds.Width / 2f < 0;
    }

    public override bool IsOutOfBounds() 
    {
        return
            Position.Y - LocalBounds.Width / 2f > SCREEN_HEIGHT;
    }

    public override void OnCollision(Scene scene)
    {
        Health--;
        if (Health <= 0)
        {
            Explode(scene);
            scene.Events.PublishScoreIncrease(50);
            Destroy(scene);
        }
        HasAlreadyCollided = true;
    }

    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        Move(dt);
        ShootTimer += dt;
        if (ShootTimer > ShotCooldown)
        {
            Random random = new Random();
            if (random.Next(0,100) <= _fireChance) Shoot(scene, this);
            ShootTimer = 0;
        }
    }
}