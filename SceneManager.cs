using static Invaders.SceneName;
namespace Invaders;

public class SceneManager
{
    public Scene _currentScene;
    public SceneManager()
    {
        _currentScene = new HomeScene(); // TODO Känns inte som detta är rätt.
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
    Waiting,
    Quit,
}