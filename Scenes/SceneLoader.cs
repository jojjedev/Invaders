using SFML.Window;
using static Invaders.SceneName;

namespace Invaders;

public sealed class SceneLoader
{
    private SceneName _currentScene = Waiting;
    private SceneName _nextScene = Waiting;
    private Dictionary<SceneName, Func<Scene>> loaders;
    public SceneLoader()
    {
        loaders = new Dictionary<SceneName, Func<Scene>>
        {
            { Home, () => new HomeScene() },
            { Game, () => new GameScene() },
            { Scoreboard, () => new ScoreboardScene() }
        };
    }

    private void OnSceneChange(SceneName sceneName)
    {
        Create(sceneName, out Scene created);
        created.Loader.Load(sceneName);
    }
    public void HandleSceneLoad(Scene scene)
    {
        if (_nextScene == Waiting) return;
        scene.Clear();
        _nextScene = Waiting;
    }
    private void Create(SceneName name, out Scene created)
    {
        if (loaders.TryGetValue(name, out Func<Scene> loader))
        {
            created = loader();
            return ;
        }

        created = null;
    }
    
    public void Load(SceneName sceneName)
    {
        _nextScene = sceneName;
    }
    
    public void Reload() => _nextScene = _currentScene;
}