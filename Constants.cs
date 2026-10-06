using SFML.Graphics;

namespace Invaders;

public abstract class Constants
{
    public const int SCREEN_WIDTH = 512;
    public const int SCREEN_HEIGHT = 1024;
    public static readonly IntRect BluePlayerShipRect = new IntRect(211, 941, 99, 75);
    public static readonly IntRect GreenPlayerShipRect = new IntRect(237, 377, 99, 75);
    public static readonly IntRect PlayerBulletRect = new IntRect(849, 310, 9, 54);

    public static readonly IntRect BlueEnemyShipRect = new IntRect(222, 0, 103, 84);
    public static readonly IntRect RedEnemyShipRect = new IntRect(224, 580, 103, 84);
    public static readonly IntRect GreenEnemyShipRect = new IntRect(224, 496, 103, 84);
    public static readonly IntRect BlackEnemyShipRect = new IntRect(144, 156, 103, 84);
    public static readonly IntRect EnemyBulletRect = new IntRect(858, 230, 9, 54);
}