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
        SetSpawnStats(ship, side);
        Affiliation = ship.Affiliation;
    }
    public bool Invulnerable { get; set; }
    public Team Affiliation { get; set; }

    public override void Create(Scene scene)
    {
        if (!scene.InteractableEntities.ContainsKey(Affiliation))
        {
            scene.InteractableEntities.Add(Affiliation, new List<Entity>());
        }
        scene.InteractableEntities[Affiliation].Add(this);
        base.Create(scene);
        
    }

    public override void Destroy(Scene scene)
    {
        base.Destroy(scene);
    }

    private void SetSpawnStats(Ship ship, Side side)
    {
        Sprite.Origin = new Vector2f(Bounds.Width * 0.5f, Bounds.Height * 0.5f);
        Sprite.Rotation = ship.Rotation;
        _direction = new Vector2f(
            MathF.Cos((Sprite.Rotation - 90) * Single.Pi / 180f),
            MathF.Sin((Sprite.Rotation - 90) * Single.Pi / 180f));
        
        Vector2f offset = new Vector2f(
            _direction.X * ship.Bounds.Width / 3.5f,
            _direction.Y * ship.Bounds.Height / 3.5f
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

        _speed = 500;

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
        return Position.X > SCREEN_WIDTH + Bounds.Width ||
               Position.X < 0 - Bounds.Width ||
               Position.Y > SCREEN_HEIGHT + Bounds.Height ||
               Position.Y < 0 - Bounds.Height;
    }

    public override void Update(Scene scene, float dt)
    {
        Move(dt);
        base.Update(scene, dt);
        if (!IsOutOfBounds())
        {
            Console.WriteLine("True");
            Destroy(scene);
        }

        
    }
}

public enum Side
{
    Left,
    Right
}