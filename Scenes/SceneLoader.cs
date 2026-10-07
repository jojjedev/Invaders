using static Invaders.SceneName;

namespace Invaders;

public sealed class SceneLoader
{
    private GUI _gui;
    private SceneName _currentScene;
    private SceneName _nextScene;
    private Dictionary<SceneName, Func<Scene>> loaders;

    public SceneLoader()
    {
        _gui = new GUI();
        loaders = new Dictionary<SceneName, Func<Scene>>
        {
            { Home, () => new HomeScene() },
            { Game, () => new GameScene() },
            { Scoreboard, () => new ScoreboardScene() }

        };
    }

    public void HandleSceneLoad(Scene scene)
    {
        if (_nextScene == Waiting) return;
        scene.Clear();
        Create(_nextScene, out Scene created);
        Console.WriteLine($"{_nextScene}");
        _nextScene = Waiting;
    }

    private bool Create(SceneName name, out Scene created)
    {
        if (loaders.TryGetValue(name, out Func<Scene> loader))
        {
            created = loader();
            return true;
        }

        created = null;
        return false;
    }
    
    
    public void Load(SceneName sceneName)
    {
        _nextScene = sceneName;
    }
    
    public void Reload() => _nextScene = _currentScene;
}