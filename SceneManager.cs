using static Invaders.SceneName;
using SFML.Window;
namespace Invaders;

public sealed class SceneManager
{
    public Scene CurrentScene;
    public int GameState = 1; // TODO: Kolla över om det behöver vara en int
    public SceneManager()
    {
        CurrentScene = new HomeScene();
        CurrentScene.Events.SceneChange += LoadScene;
    }

    private void LoadScene(SceneName sceneName)
    {
        switch (sceneName)
        {
            case Home :
                CurrentScene.Events.SceneChange -= LoadScene;
                CurrentScene = new HomeScene();
                CurrentScene.Events.SceneChange += LoadScene;
                break;
            case Scoreboard:
                CurrentScene.Events.SceneChange -= LoadScene;
                CurrentScene = new ScoreboardScene(); 
                CurrentScene.Events.SceneChange += LoadScene;

                break;
            case Game:
                CurrentScene.Events.SceneChange -= LoadScene;
                CurrentScene = new GameScene();
                CurrentScene.Events.SceneChange += LoadScene;
                break;
            case Waiting:
                break;
            case Quit:
                GameState--;
                //TODO: Kalla på funktioner som sparar score i txt fil.
                CurrentScene.Events.SceneChange -= LoadScene;
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