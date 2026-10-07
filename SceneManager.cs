using static Invaders.SceneName;
namespace Invaders;

public class SceneManager
{
    private Scene _currentScene;
    public SceneManager()
    {
        LoadScene(Home);
    }

    public void LoadScene(SceneName sceneName)
    {
        _currentScene.Loader.Load(sceneName);
    }
    
}

public enum SceneName
{
    Home,
    Scoreboard,
    Game,
}