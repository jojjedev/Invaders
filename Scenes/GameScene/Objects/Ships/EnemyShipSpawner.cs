using System.Data;
using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public sealed class EnemyShipSpawner
{
    private string _state;
    private int _level, _index;
    private float _spawnTimer = 0;
    private readonly IntRect _bounds = new IntRect(0, 0, BlueEnemyShipRect.Width, BlueEnemyShipRect.Height);
    private readonly string[] _shipColors;
    private readonly Dictionary<string, IntRect> _shipTextures;
    private float _spawnCd;
    private const float LowestSpawnCd = 0.5f;
    private const float InitialSpawnCd = 1.5f;
    private readonly GameScene _currentScene;
    public EnemyShipSpawner(GameScene scene)
    {
        _currentScene = scene;
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
        _spawnCd = InitialSpawnCd; 
        _currentScene.Events.ThresholdPassed += UpdateState;
    }
    public bool Dead { get; set; }
    public void Destroy()
    {
        _currentScene.Events.ThresholdPassed -= UpdateState;
    }
    private void SpawnShip()
    {
        EnemyShip newShip = new EnemyShip(
            _shipTextures[_state], 
            SetShipSpawn(), 
            SetHealth(), 
            SetSpeed(), 
            SetShotCd(),
            SetFireChance()
            );
        _currentScene.Spawn(newShip);
    }
    
    private Vector2f SetShipSpawn()
    {
        Random random = new Random();
        float spawnPos = random.Next(_bounds.Width / 2, SCREEN_WIDTH - _bounds.Width / 2);
        Vector2f spawn = new Vector2f(spawnPos, -_bounds.Width / 2f);
        return spawn;
    }

    private float UpdateSpawnCd()
    {
        return LowestSpawnCd + 1 * MathF.Pow(0.95f, _level); 
    }
    private int SetHealth()
    { 
        return 2 * _level;
    }

    private float SetSpeed()
    {
        return MathF.Min(MathF.Pow(-_level, 2) + 40 * _level + 200, 500);
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
        
    if (_state == "blue")
    {
        _level++;
        _spawnCd = UpdateSpawnCd();

    }
    }

    public void Update(float dt)
    {
        _spawnTimer += dt;
        if (_spawnTimer > _spawnCd)
        {
            SpawnShip();
            _spawnTimer = 0;
        }
    }
}

