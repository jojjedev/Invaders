using System.Runtime.InteropServices;
using SFML.Graphics;

namespace Invaders;

public sealed class GameScene : Scene
{
    private EnemyShipSpawner _enemySpawner;
    public GameScene()
    {
        InteractableEntities = new Dictionary<Team, List<Entity>>()
        {
            { Team.Enemy , new List<Entity>() },
            { Team.Player, new List<Entity>() } 
        };
        Background.SetColor(Color.Cyan);
        GameGUI gui = new GameGUI();
        PlayerShip player = new PlayerShip();
        _enemySpawner = new EnemyShipSpawner(this);
        Spawn(Background);
        Spawn(gui);
        Spawn(player);
        
        Events.GameOver += OnGameOver;
    }
    private void OnGameOver(Scene scene)
    {
        //TODO: Stoppa alla rörelser, ladda in ruta med text så spelare kan skriva in sitt namn.
    }
    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        _enemySpawner.Update(this, dt);
        Events.CheckLoseHealthEvent(this);  
        Events.CheckThresholdEvent();
    }
}