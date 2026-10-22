using static Invaders.SceneName;
using SFML.Window;
namespace Invaders;

public sealed class SceneManager
{
    public Scene CurrentScene;
    public int GameState = 1;
    private readonly Dictionary<SceneName, Func<Scene>> _loaders;

    public SceneManager()
    {
        _loaders = new Dictionary<SceneName, Func<Scene>>
        {
            { Home, () => new HomeScene() },
            { Game, () => new GameScene() },
            { Scoreboard, () => new ScoreboardScene() },
            { Quit,  () =>
                {
                    GameState = 0;
                    CurrentScene?.ClearObjects();
                    return CurrentScene;
                }
            }
        };
        CurrentScene = _loaders[Home].Invoke(); 
        CurrentScene.Events.SceneChange += LoadScene;
    }

    private void LoadScene(SceneName sceneName)
    {
        CurrentScene.Events.SceneChange -= LoadScene;
        CurrentScene = _loaders[sceneName].Invoke();
        CurrentScene.Events.SceneChange += LoadScene;
    }
    
}

public enum SceneName
{
    Home,
    Scoreboard,
    Game,
    Waiting,
    Quit,
}