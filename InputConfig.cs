using SFML.Window;

namespace Invaders;

public sealed class InputConfig
{
    public readonly Keyboard.Key Left;
    public readonly Keyboard.Key Right;
    public readonly Keyboard.Key Up;
    public readonly Keyboard.Key Down;
    public readonly Keyboard.Key RotateLeft;
    public readonly Keyboard.Key RotateRight;
    public readonly Keyboard.Key Shoot;
    
    public InputConfig(
        Keyboard.Key left, 
        Keyboard.Key right, 
        Keyboard.Key up, 
        Keyboard.Key down, 
        Keyboard.Key rotateLeft, 
        Keyboard.Key rotateRight,
        Keyboard.Key shoot
        )
    {
        Left = left;
        Right = right;
        Up = up;
        Down = down;
        RotateLeft = rotateLeft;
        RotateRight = rotateRight;
        Shoot = shoot;
    }
}