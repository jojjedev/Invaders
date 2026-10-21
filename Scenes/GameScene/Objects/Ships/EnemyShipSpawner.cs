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
    private readonly float _lowestSpawnCd = 0.5f;
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
        _spawnCd = _lowestSpawnCd + 1 * MathF.Pow(0.95f, _level); 
        scene.Events.ThresholdPassed += UpdateState;
    }
    public bool Dead { get; set; }
    public void Destroy(Scene scene)
    {
        scene.Events.ThresholdPassed -= UpdateState;
    }
    private void SpawnShip(Scene scene)
    {
        EnemyShip newShip = new EnemyShip(
            _shipTextures[_state], 
            SetShipSpawn(), 
            SetHealth(), 
            SetSpeed(), 
            SetShotCd(),
            SetFireChance()
            );
        scene.Spawn(newShip);
        Console.WriteLine($"\nNew Ship at level {_level}\nHealth: {SetHealth()}\nSpeed: {SetSpeed()}\nShotCd: {SetShotCd()}\nFireChance: {SetFireChance()}");
    }
    
    private Vector2f SetShipSpawn()
    {
        Random random = new Random();
        float spawnPos = random.Next(bounds.Width / 2, SCREEN_WIDTH - bounds.Width / 2);
        Vector2f spawn = new Vector2f(spawnPos, -bounds.Width / 2f);
        return spawn;
    }
    
    private int SetHealth()
    { 
        return 2 * _level;
    }

    private float SetSpeed()
    {
        return MathF.Min(MathF.Pow(-_level, 2) + 40 * _level + 300, 800);
    }

    private float SetShotCd()
    {
        return 0.5f + 0.25f * MathF.Pow(0.90f, _level);
    }

    private float SetFireChance()
    {
        return MathF.Max(80, 30 + 5 * _level);
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

