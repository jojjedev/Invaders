using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard.Key;


namespace Invaders;

public abstract class Scene
{
    private bool DEBUG = true; // TODO: TRUE = AKTIVE, FALSE = INAKTIVE. TA BORT INNAN INLÄMNING
    
    public readonly AssetManager Assets;
    public readonly SceneLoader Loader;
    public readonly EventManager Events;
    public Dictionary<Team, List<Entity>> InteractableEntities;
    private List<IOnScreenObject> _objects;
    protected Background Background;
    protected bool KeyPressed;
    protected float Timer;

    protected Scene()
    {
        Assets = new AssetManager();
        Loader = new SceneLoader();
        Events = new EventManager();
        _objects = new List<IOnScreenObject>();
        Background = new Background();
    }
    protected virtual void LoadNextScene(int target){}
    
    public virtual void Spawn(IOnScreenObject onScreenObject)
    {
        _objects.Add(onScreenObject);
        onScreenObject.Create(this);
    }
    
    public virtual void Clear()
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
    public void ClearAll()
    {
        for (int i = 0; i < _objects.Count; i++)
        {
            IOnScreenObject onScreenObject = _objects[i];
            onScreenObject.DontClearOnLoad = false;
            _objects.RemoveAt(i);
            onScreenObject.Destroy(this);
        }
    }
    
    
    // All IInteractables are Entities
    /// <summary>
    /// Each "Enemy" object is checking collisions with each object on team "Player"
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    public IEnumerable<Entity> CollisionCheck(FloatRect bounds)
    {
        int lastObjOnTeamPlayer = InteractableEntities[Team.Player].Count - 1;
        for (int i = lastObjOnTeamPlayer; i >= 0; i--)
        {
            Entity entity = InteractableEntities[Team.Player][i];
            if (entity.Dead) continue;
            if (entity.Bounds.Intersects(bounds))
            {
                yield return entity;
            }
        }
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
            foreach (Team team in InteractableEntities.Keys)
            {
                foreach (Entity entity in InteractableEntities[team])
                {
                    if (entity is IInteractable)
                    {
                        DebugShowHitboxes(entity, target);
                    }
                }
            }
        };
    }


    public void DebugShowHitboxes(Entity e, RenderTarget target)
    {
        RectangleShape rectangle = new RectangleShape(new Vector2f(e.Bounds.Width, e.Bounds.Height));
        rectangle.Position = e.Position;
        rectangle.Rotation = e.Rotation;
        rectangle.Origin = new Vector2f(e.Bounds.Width * 0.5f, e.Bounds.Height * 0.5f);
        rectangle.OutlineColor = Color.White;
        rectangle.OutlineThickness = 1;
        rectangle.FillColor = Color.Transparent;
        
        target.Draw(rectangle);
        
    }
}