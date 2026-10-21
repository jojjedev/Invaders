using System.Runtime.InteropServices;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace Invaders;

public sealed class GameScene : Scene
{
    private readonly EnemyShipSpawner _enemySpawner;

    public GameScene()
    {
        InteractableEntities = new Dictionary<Team, List<IInteractable>>()
        {
            { Team.Enemy , new List<IInteractable>() },
            { Team.Player, new List<IInteractable>() } 
        };
        PlayerShip player = new PlayerShip();
        _enemySpawner = new EnemyShipSpawner(this);
        Spawn(Background);
        Spawn(player);
        GameGUI gui = new GameGUI(this);
        Spawn(gui);
        Music(false);
        Events.GameOver += OnGameOver;
    }
    private void Music(bool onOff)
    {
        Sound music = Assets.PlaySound("battle", 3, true);
        if (onOff) music.Dispose();
    }
    private void OnGameOver()
    {
        ClearAllInteractableEntities();
        Music(true);
    }

    private void ClearAllInteractableEntities()
    {
        foreach (Team team in InteractableEntities.Keys)
        {
            foreach (IInteractable inter in InteractableEntities[team])
            {
                inter.ToBeDestroyed = true;
            }
        }

        _enemySpawner.Dead = true; 
        _enemySpawner.Destroy();
    }
    /// <summary>
    /// Each interactable object is checking collisions with each Team and gets added to the returned collection of objects
    /// that has collided.
    /// </summary>
    /// <param name="bounds"></param>
    /// <returns></returns>
    private IEnumerable<IInteractable> CollisionCheck(IInteractable interactable)
    {
        Team[] teams = Enum.GetValues<Team>();
        foreach (Team team in teams)
        {
            if (interactable.Affiliation != team)
            {
                Team otherTeam = team;
                int lastObjOnOtherPlayer = InteractableEntities[otherTeam].Count - 1;
                for (int i = lastObjOnOtherPlayer; i >= 0; i--)
                {
                    IInteractable otherInter = InteractableEntities[otherTeam][i];
                    if (otherInter.ToBeDestroyed) continue;
                    if (otherInter.CollisionBounds.Intersects(interactable.CollisionBounds))
                    {
                        yield return otherInter;
                    }
                }
            } 
        }
    }
    /// <summary>
    /// For each entity in each Team, search for collisions with objects of the other teams. If the other entity
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
                foreach (IInteractable otherTeamInteractable in CollisionCheck(e))
                {
                    if (e is Bullet && otherTeamInteractable is Bullet) continue;
                    if (!e.Invulnerable && !otherTeamInteractable.Invulnerable)
                    {
                        otherTeamInteractable.OnCollision();
                        e.OnCollision();
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

        if (InteractableEntities.Keys.Count >= 2) CheckAllCollisions();
        if (!_enemySpawner.Dead) _enemySpawner.Update(dt);
        Events.CheckValueChangeEvent();  
        Events.CheckThresholdEvent();
        Events.CheckGameOverEvent();
    }
}