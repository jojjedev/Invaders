using System.Data;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public class EnemyShipSpawner
{
    private IntRect bounds = new IntRect(0, 0, BlueEnemyShipRect.Width, BlueEnemyShipRect.Height);
    private readonly string[] _shipColors;
    private Dictionary<string, IntRect> _shipTextures;
    private string _state;
    private int _level;
    private int _index;
    private float _spawnCd;
    private float _spawnTimer = 0;
    public EnemyShipSpawner(Scene scene)
    {
        _shipColors = new string[] { "blue", "green", "red", "black" };
        _shipTextures = new Dictionary<string, IntRect>
        {
            { "blue", BlueEnemyShipRect },
            { "green", GreenEnemyShipRect },
            { "red", RedEnemyShipRect },
            { "black", BlackEnemyShipRect },
        };
        _state = "blue";
        _index = 0;
        _level = 1;
        _spawnCd = 2 + 2 * MathF.Pow(0.95f, _level);
        scene.Events.ThresholdPassed += UpdateState;
    }

    public void Destroy(Scene scene)
    {
        scene.Events.ThresholdPassed -= UpdateState;
    }
    public void SpawnShip(Scene scene)
    {
        EnemyShip newShip = new EnemyShip(
            _shipTextures[_state], 
            SetShipSpawn(), 
            SetHealth(), 
            SetSpeed(), 
            SetShotCd()
            );
        scene.Spawn(newShip);
    }
    
    private Vector2f SetShipSpawn()
    {
        Random random = new Random();
        float spawnPos = random.Next(bounds.Width / 2, SCREEN_WIDTH - bounds.Width / 2);
        Vector2f spawn = new Vector2f(spawnPos, bounds.Width);
        spawn = new Vector2f(256, 100);
        return spawn;
    }
    
    private int SetHealth()
    { 
        return 2 * _level;
    }

    private float SetSpeed()
    {
        return 200 * MathF.Pow(1.10f, _level);
    }

    private float SetShotCd()
    {
        return 0.5f + 0.5f * MathF.Pow(0.90f, _level);
    }

    private void UpdateState()
    {
        _index++;
        _state = _shipColors[_index % 4];
    if (_state == "blue") _level++;
    }

    public void Update(Scene scene, float dt)
    {
        _spawnTimer += dt;
        if (_spawnTimer > _spawnCd)
        {
            SpawnShip(scene);
            _spawnTimer = 0;
        }
    }
}

