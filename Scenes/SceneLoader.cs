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
            { SceneName.Home, () => new HomeScene() },
            { SceneName.Game, () => new GameScene() },
            { SceneName.Scoreboard, () => new ScoreboardScene() }

        };
    }

    public void HandleSceneLoad(Scene scene)
    {
       
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