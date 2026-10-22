using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;
namespace Invaders;

public sealed class Bullet : Entity, IInteractable
{
    private Vector2f _direction;
    private float _speed;

    public Bullet(Ship ship, Side side) : base("sheet")
    {
        Dictionary<Team, Func<IntRect>> teamTexture = new Dictionary<Team, Func<IntRect>>()
        {
            { Team.Player, () => Sprite.TextureRect = PlayerBulletRect },
            { Team.Enemy, () => Sprite.TextureRect = EnemyBulletRect }
        };
        teamTexture[ship.Affiliation].Invoke();
        Affiliation = ship.Affiliation;
        Rotation = Affiliation == Team.Player ? ship.Rotation : ship.Rotation + 180;
        SetSpawnStats(ship, side);
    }
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
    public bool ToBeDestroyed
    {
        get => Dead;
        set => Dead = value;
    }
   
    public FloatRect CollisionBounds =>
        new FloatRect(Sprite.Position - Sprite.Origin,
            new Vector2f(LocalBounds.Width, LocalBounds.Width)
        );
    public override float Rotation { get; set; }
    public override FloatRect DebugBounds => CollisionBounds;


    public void OnCollision()
    {
        Invulnerable = true;
        ToBeDestroyed = true;
    }

    public override void Destroy()
    {
        ToBeDestroyed = true;
    }
    
    private void SetSpawnStats(Ship ship, Side side)
    {
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, 0);
        Sprite.Rotation = Rotation;
        Vector2f offset = new Vector2f(
            MathF.Cos(Sprite.Rotation  * Single.Pi / 180f) * ship.LocalBounds.Width / 3.5f,
            MathF.Sin(Sprite.Rotation  * Single.Pi / 180f) * ship.LocalBounds.Height / 3.5f
            );
        switch (side)
        {
            case Side.Left: 
                Position = new Vector2f(
                    ship.Position.X - offset.X,
                    ship.Position.Y - offset.Y
                );
                break;
            case Side.Right:
                Position = new Vector2f(
                    ship.Position.X + offset.X,
                    ship.Position.Y + offset.Y
                );
                break;
        }

        _speed = 800;
        _direction = new Vector2f(MathF.Cos((Rotation - 90) * Single.Pi / 180f), MathF.Sin((Rotation - 90) * Single.Pi / 180f));
    }

    private void Move(float dt)
    {
        Vector2f velocity = _direction * _speed;
        Vector2f newPos = Position;
        newPos += velocity * dt;
        Position = newPos;
    }

    private bool IsOutOfBounds()
    {
        return Position.X > SCREEN_WIDTH + LocalBounds.Width / 2f ||
               Position.X < 0 - LocalBounds.Width / 2f ||
               Position.Y > SCREEN_HEIGHT + LocalBounds.Height / 2f ||
               Position.Y < 0 - LocalBounds.Height / 2f;
    }
    
    public override void Update(float dt)
    {
        Move(dt);
        base.Update(dt);
        if (IsOutOfBounds())
        {
            Destroy();
        }
    }
}

public enum Side
{
    Left,
    Right
}