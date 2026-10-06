using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Scene
{
    private List<Entity> _entities;
    public readonly AssetManager Assets;
    public readonly SceneLoader Loader;
    public readonly EventManager Events;
    public Scene()
    {
        _entities = new List<Entity>();
        Assets = new AssetManager();
        Loader = new SceneLoader();
        Events = new EventManager();
    }

    public void Spawn(Entity entity)
    {
        _entities.Add(entity);
        entity.Create(this);
    }

    public void Clear()
    {
        for (int i = _entities.Count - 1; i >= 0; i--)
        {
            Entity entity = _entities[i];
            if (entity.DontClearOnLoad)
            {
                _entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }

    public void UpdateAll(float dt)
    {
        Loader.HandleSceneLoad(this);
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
        Events.CheckEvent(this);
    }

    public void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            _entities[i].Render(target);
        }
    }
}