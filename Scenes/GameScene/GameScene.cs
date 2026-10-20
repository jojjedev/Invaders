using System.Runtime.InteropServices;
using SFML.Graphics;
using SFML.Window;

namespace Invaders;

public sealed class GameScene : Scene
{
    private EnemyShipSpawner _enemySpawner;
    public GameScene()
    {
        InteractableEntities = new Dictionary<Team, List<IInteractable>>()
        {
            { Team.Enemy , new List<IInteractable>() },
            { Team.Player, new List<IInteractable>() } 
        };
        Background.SetColor(Color.Cyan);
        GameGUI gui = new GameGUI(this);
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
    
    /// <summary>
    /// Each "Enemy" object is checking collisions with each object on team "Player"
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    private IEnumerable<IInteractable> CollisionCheck(IInteractable interactable)
    {
        Team otherTeam;
        if (interactable.Affiliation == Team.Enemy) otherTeam = Team.Player;
        else otherTeam = Team.Enemy;
        int lastObjOnOtherPlayer = InteractableEntities[otherTeam].Count - 1;
        for (int i = lastObjOnOtherPlayer; i >= 0; i--)
        {
            IInteractable otherInter = InteractableEntities[otherTeam][i];
            if (otherInter.ToBeDestroyed) continue;
            //TODO: Cirkelkollision mellan olika skepp.
            if (otherInter.CollisionBounds.Intersects(interactable.CollisionBounds))
            {
                yield return otherInter;
            }
        }
    }
    /// <summary>
    /// For each entity in each Team, search for collisions with objects of the other team. If the other entity
    /// is not marked as ToBeDestroyed, call that entity's collision handling function.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="dt"></param>
    private void CheckAllCollisions()
    {
        foreach (Team team in InteractableEntities.Keys)
        {
            foreach (IInteractable e in InteractableEntities[team])
            {
                if (!e.Invulnerable)
                {
                    foreach (IInteractable otherTeamInteractable in CollisionCheck(e))
                    {
                        otherTeamInteractable.OnCollision(this);
                        if (e is Bullet && otherTeamInteractable is Ship) e.ToBeDestroyed = true;
                    }    
                }
                
            }
        }
    }
    public override void UpdateAll(float dt)
    {
        base.UpdateAll(dt);
        foreach (Team team in InteractableEntities.Keys)
        {
            for (int i = 0; i < InteractableEntities[team].Count; i++)
            {
                if (InteractableEntities[team][i].ToBeDestroyed)
                {
                    InteractableEntities[team].RemoveAt(i);
                    
                }
            }
        }

        
        CheckAllCollisions();
        _enemySpawner.Update(this, dt);
        Events.CheckValueChangeEvent(this);  
        Events.CheckThresholdEvent();
        if (Keyboard.IsKeyPressed(Keyboard.Key.R))
        {
            PlayerShip player = new PlayerShip();
            Spawn(player);
        }
            
    }
}