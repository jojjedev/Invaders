using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity : IOnScreenObject
{
    protected string TextureName;
    protected Sprite Sprite;

    protected Entity(string textureName)
    {
        this.TextureName = textureName;
        Sprite = new Sprite();
    }
    public virtual bool Dead { get; set; }
    public bool DontClearOnLoad { get; set; }

    public virtual void Create(Scene scene) 
    {
        Sprite.Texture = scene.Assets.LoadTexture(TextureName);
    }

    public virtual void Destroy(Scene scene) { }

    public virtual void Update(Scene scene, float dt) { }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(Sprite);
    }
}