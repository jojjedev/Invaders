using static Invaders.SceneName;
namespace Invaders;

public class SceneManager
{
    public Scene CurrentScene = new HomeScene();
    public SceneManager()
    {
        LoadScene(Home);
        CurrentScene.Events.SceneChange += LoadScene;
    }

    public void LoadScene(SceneName sceneName)
    {
        CurrentScene.Loader.Load(sceneName);
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