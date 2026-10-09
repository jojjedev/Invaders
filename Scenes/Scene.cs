using SFML.Graphics;
using SFML.System;

namespace Invaders;

public abstract class Scene
{
    protected List<Entity> _entities;
    public readonly AssetManager Assets;
    public readonly SceneLoader Loader;
    public readonly EventManager Events;
    protected Background _background;
    private SceneName _sceneName;
    public Scene()
    {
        Assets = new AssetManager();
        Loader = new SceneLoader();
        Events = new EventManager();
        _entities = new List<Entity>();
        _background = new Background();
        Spawn(_background);
    }
    
    public virtual void Spawn(Entity entity)
    {
        _entities.Add(entity);
        entity.Create(this);
    }

    public virtual void Clear()
    {
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            Entity entity = _entities[i];
            if (!entity.DontClearOnLoad)
            {
                _entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }

    public virtual void UpdateAll(float dt)
    {

        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            Entity entity = _entities[i];
            entity.Update(this, dt);
        }

        for (int i = 0; i < _entities.Count; i++)
        {
            Entity entity = _entities[i];
            if (entity.Dead) _entities.RemoveAt(i);
            else i++;
        }
    }

    public virtual void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            _entities[i].Render(target);
        }
    }
}