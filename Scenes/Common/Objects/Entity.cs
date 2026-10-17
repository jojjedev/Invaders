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

    public Sprite Sprite { get; set; }
    public virtual bool Dead { get; set; }
    public bool DontClearOnLoad { get; set; }
    public virtual Vector2f Position
    {
        get => Sprite.Position;
        set => Sprite.Position = value;
    }    
    public virtual FloatRect LocalBounds => Sprite.GetLocalBounds();
    public virtual FloatRect GlobalBounds => Sprite.Transform.TransformRect(LocalBounds);
    public virtual int Rotation { get; set; }
    

    public virtual void Create(Scene scene) 
    {
        Sprite.Texture = scene.Assets.LoadTexture(_textureName);
    }

    public virtual void Destroy(Scene scene)
    {
        Dead = true;
    }

    protected virtual void OnCollision(Scene scene){}
    public virtual void Update(Scene scene, float dt)
    {
        foreach (Entity e in scene.InteractableEntities[Team.Enemy])
        {
            foreach (Entity playerEntity in scene.CollisionCheck(e.LocalBounds))
            {
                playerEntity.OnCollision(scene);
            }
        }
    }

    public virtual void Render(RenderTarget target)
    {
        target.Draw(Sprite);
    }
}