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

    public Color Color
    {
        get => _sprite.Color;
        set => _sprite.Color = value;
    }
    public Vector2f Position
    {
        get => _sprite.Position;
        set => _sprite.Position = value;
    }
    public virtual FloatRect Bounds => _sprite.GetGlobalBounds();

    public virtual bool DontClearOnLoad { get; set; }
    public virtual bool Invulnerable { get; set; }

    public virtual void Create(Scene scene)
    {
        _sprite.Texture = scene.Assets.LoadTexture(textureName);
    }
    public virtual void Destroy(Scene scene){}
    
    public virtual void Update(Scene scene, float dt){}
    public virtual void Render(RenderTarget target)
    {
        target.Draw(_sprite);
    }
}