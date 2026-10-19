using SFML.Window;
using static Invaders.SceneName;

namespace Invaders;

public sealed class SceneLoader
{
    private SceneName _currentScene = Waiting;
    private SceneName _nextScene = Waiting;
    private Dictionary<SceneName, Func<Scene>> _loaders;
    public SceneLoader()
    {
        _loaders = new Dictionary<SceneName, Func<Scene>>
        {
            { Home, () => new HomeScene() },
            { SceneName.Game, () => new GameScene() },
            { Scoreboard, () => new ScoreboardScene() }
        };
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (_nextScene == Waiting) return;
        scene.Clear();
        
        // TODO: Kanske behövs i GameScene, annars ta bort.
        _nextScene = Waiting;
    }
    private void Create(SceneName name, out Scene created)
    {
        if (_loaders.TryGetValue(name, out Func<Scene> loader))
        {
            created = loader();
            return ;
        }

        created = null;
    }
    
    private void Load(SceneName sceneName)
    {
        _nextScene = sceneName;
    }
    
    public void Reload() => _nextScene = _currentScene;
}