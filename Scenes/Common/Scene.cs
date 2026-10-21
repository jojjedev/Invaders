using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public abstract class Scene
{
    private bool DEBUG = false; // TODO: TRUE = AKTIVE, FALSE = INAKTIVE. TA BORT INNAN INLÄMNING
    
    public readonly AssetManager Assets;
    public readonly EventManager Events;
    private readonly List<IOnScreenObject> _objects;
    protected Dictionary<Team, List<IInteractable>> InteractableEntities;
    protected readonly Background Background;
    protected bool KeyPressed;
    protected float Timer;

    protected Scene()
    {
        Assets = new AssetManager();
        Events = new EventManager();
        _objects = new List<IOnScreenObject>();
        Background = new Background();
    }
    
    public virtual void Spawn(IOnScreenObject onScreenObject)
    {
        _objects.Add(onScreenObject);
        onScreenObject.Create(this);
        if (onScreenObject is IInteractable)
        {
            IInteractable inter = (IInteractable)onScreenObject;
            if (!InteractableEntities.ContainsKey(inter.Affiliation))
            {
                InteractableEntities.Add(inter.Affiliation, new List<IInteractable>());
            }
            InteractableEntities[inter.Affiliation].Add(inter);
        }
    }
    
    protected virtual void Clear()
    {
        for (int i = _objects.Count - 1; i >= 0; i--)
        {
            IOnScreenObject onScreenObject = _objects[i];
            if (!onScreenObject.DontClearOnLoad)
            {
                _objects.RemoveAt(i);
                onScreenObject.Destroy(this);
            }
        }
    }
    public void ClearObjects()
    {
        for (int i = _objects.Count - 1; i >= 0 ; i--)
        {
            IOnScreenObject onScreenObject = _objects[i];
            onScreenObject.DontClearOnLoad = false;
            _objects.RemoveAt(i);
            onScreenObject.Destroy(this);
        }
    }
    
    public bool FindByType<T>(out T found) where T : IInteractable
    {
        foreach (Team team in InteractableEntities.Keys)
        {
            foreach (IInteractable inter in InteractableEntities[team])
            {
                if (!inter.ToBeDestroyed && inter is T typed)
                {
                    found = typed;
                    return true;
                }
            }
        }
        found = default(T);
        return false;
    }
    
    public virtual void UpdateAll(float dt)
    {
        for (int i = _objects.Count - 1; i >= 0; i--)
        {
            _objects[i].Update(this, dt);
        }
        for (int i = 0; i < _objects.Count; i++)
        {
            IOnScreenObject screenObject = _objects[i];
            if (_objects[i].Dead)
            {
                _objects.RemoveAt(i);
                screenObject.Destroy(this);
            }
        }
        Events.CheckSceneNameEvent();
        Timer += dt;

        if (Keyboard.IsKeyPressed(B)) DEBUG = true;
        else DEBUG = false;
    }

    public virtual void RenderAll(RenderTarget target)
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            _objects[i].Render(target);
        }
        // DEBUG
        if (DEBUG)
        {
            Debug(target);
        }
    }

    private void Debug(RenderTarget target)
    {
        foreach (Team team in InteractableEntities.Keys)
        {
            foreach (Entity entity in InteractableEntities[team])
            {
                DebugShowHitboxes(entity, target);
            }
        }
    }
    
    private void DebugShowHitboxes(Entity e, RenderTarget target)
    {
        RectangleShape rectangle = new RectangleShape(new Vector2f(e.DebugBounds.Width, e.DebugBounds.Height));
        rectangle.Position = e.Position;
       // rectangle.Rotation = e.Rotation;
        rectangle.Origin = new Vector2f(e.DebugBounds.Width * 0.5f, e.DebugBounds.Height * 0.5f);
        rectangle.OutlineColor = Color.White;
        rectangle.OutlineThickness = 1;
        rectangle.FillColor = Color.Transparent;
        
        if (!e.Dead)target.Draw(rectangle);
        
    }
}