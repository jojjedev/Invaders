using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Explosion : Entity
{
    private float _timer;
    private float _duration;
    private int _textureWidth;
    private int _textureHeight;
    private int _textureTilesCount;
    public Explosion(Entity entity) : base("explosion")
    {
        Position = entity.Position;
        Sprite.Scale = new Vector2f(2, 2);
        _duration = 1f / 3f;
    }

    public override void Create(Scene scene)
    {
        base.Create(scene);
        _textureTilesCount = 7;
        _textureWidth = (int)Sprite.Texture.Size.X / _textureTilesCount;
        _textureHeight = (int)Sprite.Texture.Size.Y;
        Sprite.Origin = new Vector2f(_textureWidth / 2f, _textureHeight / 2f);
        GetTextureByIndex(GetIndex());
    }

    private void GetTextureByIndex(int index)
    {
        Sprite.TextureRect = new IntRect(index * _textureWidth, 0, _textureWidth, _textureHeight);
    }

    private int GetIndex()
    {
        int index;
        // Från föreläsning 18 slide 55;
        index = Math.Min((int)(_timer * 3 * _textureTilesCount), _textureTilesCount - 1);
        
        return index;
    }
    public override void Update(Scene scene, float dt)
    {
        base.Update(scene, dt);
        _timer += dt;
        if (_timer < _duration)
        {
            GetTextureByIndex(GetIndex());
        }
        else  Destroy(scene);
    }
}