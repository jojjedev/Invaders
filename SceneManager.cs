using static Invaders.SceneName;
using SFML.Window;
namespace Invaders;

public class SceneManager
{
    public Scene CurrentScene;
    public int GameState = 1;
    public SceneManager()
    {
        CurrentScene = new HomeScene();
        CurrentScene.Events.SceneChange += LoadScene;
    }

    public void LoadScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case Home :
                CurrentScene = new HomeScene();
                break;
            case Scoreboard:
                CurrentScene = new ScoreboardScene();
                break;
            case Game:
                CurrentScene = new GameScene();
                break;
            case Waiting:
                break;
            case Quit:
                GameState--;
                //TODO: Kalla på funktioner som sparar score i txt fil.
                CurrentScene.ClearAll();
                break;
        }
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