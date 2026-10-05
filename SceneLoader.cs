namespace Invaders;

public sealed class SceneLoader
{
    private GUI _gui;
    private string _currentScene = "";
    private string _nextScene = "";

    public SceneLoader()
    {
        _gui = new GUI();
    }

    public void HandleSceneLoad(Scene scene)
    {
        if (_nextScene == "") return;
        
    }
}