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
    protected List<IOnScreenObject> Objects;
    protected Background Background;
    protected bool KeyPressed;
    protected float Timer;

    protected Scene()
    {
        Assets = new AssetManager();
        Loader = new SceneLoader();
        Events = new EventManager();
        Objects = new List<IOnScreenObject>();
        Background = new Background();
    }
    protected virtual void LoadNextScene(int target){}
    
    protected virtual void Spawn(IOnScreenObject onScreenObject)
    {
        Objects.Add(onScreenObject);
        onScreenObject.Create(this);
    }

    public virtual void Clear()
    {
        for (int i = Objects.Count - 1; i >= 0; i--)
        {
            IOnScreenObject onScreenObject = Objects[i];
            if (!onScreenObject.DontClearOnLoad)
            {
                Objects.RemoveAt(i);
                onScreenObject.Destroy(this);
            }
        }
    }

    public void ClearAll()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            IOnScreenObject onScreenObject = Objects[i];
            onScreenObject.DontClearOnLoad = false;
            Objects.RemoveAt(i);
            onScreenObject.Destroy(this);
        }
    }
    

    public virtual void UpdateAll(float dt)
    {
        for (int i = Objects.Count - 1; i >= 0; i--)
        {
            Objects[i].Update(this, dt);
        }
        for (int i = 0; i < Objects.Count; i++)
        {
            IOnScreenObject screenObject = Objects[i];
            if (Objects[i].Dead)
            {
                Objects.RemoveAt(i);
                screenObject.Destroy(this);
            }
        }

        Events.CheckSceneNameEvent();
        Timer += dt;
    }

    public virtual void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Objects[i].Render(target);
        }
    }
}