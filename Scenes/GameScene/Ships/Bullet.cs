using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;
namespace Invaders;

public sealed class Bullet : Entity, IMoveable, IInteractable
{
    public Dictionary<Team, Func<IntRect>> TeamTexture;
    private Vector2f _direction;
    private float _speed;

    public Bullet(Ship ship, Side side) : base("sheet")
    {
        TeamTexture = new Dictionary<Team, Func<IntRect>>()
        {
            { Team.Player, () => Sprite.TextureRect = PlayerBulletRect },
            { Team.Enemy, () => Sprite.TextureRect = EnemyBulletRect }
        };
        TeamTexture[ship.Affiliation].Invoke();
        Affiliation = ship.Affiliation;
        Rotation = Affiliation == Team.Player ? ship.Rotation : ship.Rotation + 180;
        SetSpawnStats(ship, side);
    }
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }
    public override float Rotation { get; set; }
    
    public FloatRect CollisionBounds =>
        new FloatRect(Sprite.Position - Sprite.Origin,
            new Vector2f(LocalBounds.Width, LocalBounds.Width)
        );

    public override FloatRect CustomBounds => CollisionBounds;

    public bool ToBeDestroyed
    {
        get => Dead;
        set => Dead = value;
    }
    public override void Create(Scene scene)
    {
        if (!scene.InteractableEntities.ContainsKey(Affiliation))
        {
            scene.InteractableEntities.Add(Affiliation, new List<IInteractable>());
        }
        scene.InteractableEntities[Affiliation].Add(this);
        base.Create(scene);
        
    }

    public override void Destroy(Scene scene)
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

        _speed = 1000;
        _direction = new Vector2f(MathF.Cos((Rotation - 90) * Single.Pi / 180f), MathF.Sin((Rotation - 90) * Single.Pi / 180f));
    }

    public void Move(float dt)
    {
        Vector2f velocity = _direction * _speed;
        Vector2f newPos = Position;
        newPos += velocity * dt;
        Position = newPos;
        
    }

    public bool IsOutOfBounds()
    {
        return Position.X > SCREEN_WIDTH + LocalBounds.Width ||
               Position.X < 0 - LocalBounds.Width ||
               Position.Y > SCREEN_HEIGHT + LocalBounds.Height ||
               Position.Y < 0 - LocalBounds.Height;
    }
    
    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
        if (IsOutOfBounds())
        {
            Destroy(scene);
        }
    }
}

public enum Side
{
    Left,
    Right
}