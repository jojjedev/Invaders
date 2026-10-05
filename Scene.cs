namespace Invaders;

public class Scene
{
    private AssetManager _assetManager;
    private List<Entity> _entities;
    public Scene()
    {
        _assetManager = new AssetManager();
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
    public void UpdateAll(){}
    public void RenderAll(){}
}