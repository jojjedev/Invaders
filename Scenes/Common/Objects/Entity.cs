using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Entity : IOnScreenObject
{
    private readonly string _textureName;
    protected Entity(string textureName)
    {
        this._textureName = textureName;
        Sprite = new Sprite();
    }
    protected Scene CurrentScene { get; private set; }
    protected Sprite Sprite { get; }
    public virtual bool Dead { get; set; }
    public bool DontClearOnLoad { get; set; }
    public virtual Vector2f Position
    {
        get => Sprite.Position;
        set => Sprite.Position = value;
    }    
    public virtual FloatRect LocalBounds => Sprite.GetLocalBounds();
    public virtual FloatRect DebugBounds { get; }
    
    public virtual float Rotation
    {
        get => Sprite.Rotation;
        set => Sprite.Rotation = value;
    }
    

    public virtual void Create(Scene scene)
    {
        CurrentScene = scene;
        Sprite.Texture = CurrentScene.Assets.LoadTexture(_textureName);
    }

    public virtual void Destroy()
    {
        Dead = true;
    }
    
    public virtual void Update(float dt) { }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(Sprite);
    }
}