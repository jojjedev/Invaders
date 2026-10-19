using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public sealed class EnemyShip : Ship
{
    
    public EnemyShip(IntRect textureRect, Vector2f pos, int hp, float speed, float shotCd)
    {
        Sprite.TextureRect = textureRect;
        Sprite.Origin = new Vector2f(LocalBounds.Width * 0.5f, LocalBounds.Height * 0.5f);
        Sprite.Position = pos;
        Health = hp;
        Speed = speed;
        ShotCooldown = shotCd;
    }

    public override void Move(float dt)
    {
        base.Move(dt);
    }

    public override void Create(Scene scene)
    {
        Affiliation = Team.Enemy;
        base.Create(scene);
    }
    
    public override bool IsOutOfBounds()
    {
        return
            GlobalBounds.Intersects(new FloatRect(SCREEN_WIDTH, 0, 1, SCREEN_HEIGHT)) ||
            GlobalBounds.Intersects(new FloatRect(-1, 0, 1, SCREEN_HEIGHT)) ||
            GlobalBounds.Intersects(new FloatRect(0, SCREEN_HEIGHT, 1, SCREEN_WIDTH)) ||
            GlobalBounds.Intersects(new FloatRect(0, -1, 1, SCREEN_WIDTH));
    }
    
}