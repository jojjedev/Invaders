using SFML.Graphics;
using SFML.System;

namespace Invaders;

public class Entity
{
    private readonly string textureName;
    protected readonly Sprite _sprite;
    public bool Dead;
    protected Entity(string textureName)
    {
        this.textureName = textureName;
        _sprite = new Sprite();
    }

    public Vector2f Position
    {
        get => _sprite.Position;
        set => _sprite.Position = value;
    }
    public virtual FloatRect Bounds => _sprite.GetGlobalBounds();
}