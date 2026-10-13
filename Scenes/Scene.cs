using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public abstract class Scene
{
    public readonly AssetManager Assets;
    public readonly SceneLoader Loader;
    public readonly EventManager Events;
    protected List<Entity> Entities;
    protected Background Background;
    protected bool KeyPressed;
    protected float Timer;

    protected Scene()
    {
        Assets = new AssetManager();
        Loader = new SceneLoader();
        Events = new EventManager();
        Entities = new List<Entity>();
        Background = new Background();
    }
    protected virtual void LoadNextScene(int target){}
    
    protected virtual void Spawn(Entity entity)
    {
        Entities.Add(entity);
        entity.Create(this);
    }

    public virtual void Clear()
    {
        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            Entity entity = Entities[i];
            if (!entity.DontClearOnLoad)
            {
                Entities.RemoveAt(i);
                entity.Destroy(this);
            }
        }
    }

    public void ClearAll()
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Entity entity = Entities[i];
            entity.DontClearOnLoad = false;
            Entities.RemoveAt(i);
            entity.Destroy(this);
        }
    }
    

    public virtual void UpdateAll(float dt)
    {
        for (int i = Entities.Count - 1; i >= 0; i--)
        {
            Entity entity = Entities[i];
            entity.Update(this, dt);
        }

        for (int i = 0; i < Entities.Count; i++)
        {
            Entity entity = Entities[i];
            if (entity.Dead) Entities.RemoveAt(i);
            else i++;
        }
        Events.CheckSceneNameEvent();
        Timer += dt;
    }

    public virtual void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < Entities.Count; i++)
        {
            Entities[i].Render(target);
        }
    }
}