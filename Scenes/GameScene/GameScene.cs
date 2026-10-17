using System.Runtime.InteropServices;
using SFML.Graphics;

namespace Invaders;

public sealed class GameScene : Scene
{
    
    private PlayerShip _player;
    private GameGUI _gui;
    public GameScene()
    {
        InteractableEntities = new Dictionary<Team, List<Entity>>()
        {
            { Team.Enemy , new List<Entity>() },
            { Team.Player, new List<Entity>() } 
        };
        Background.SetColor(Color.Cyan);
        _gui = new GameGUI();
        _player = new PlayerShip();
        Spawn(Background);
        Spawn(_gui);
        Spawn(_player);

        Events.GameOver += OnGameOver;
    }
    private void OnGameOver(Scene scene)
    {
        //TODO: Stoppa alla rörelser, ladda in ruta med text så spelare kan skriva in sitt namn.
    }

    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        Events.CheckLoseHealthEvent(this);
    }
}