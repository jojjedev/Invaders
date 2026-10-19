using SFML.System;
using SFML.Window;
using SFML.Graphics;
using static SFML.Window.Keyboard.Key;
namespace Invaders;


//TODO Lägg logik över grafiska representationen
public class PlayerShipHandler
{
    private float _invulnTimer;
    
    // MOVEMENT
    private void Rotate(Ship ship)
    {
        if (Keyboard.IsKeyPressed(Left)) ship.Rotation -= 2;
        if (Keyboard.IsKeyPressed(Right)) ship.Rotation += 2;
    }
    public void Move(Ship ship, float dt)
    {
        int XDirection = 0;
        int YDirection = 0;
        if (Keyboard.IsKeyPressed(A)) XDirection--;
        if (Keyboard.IsKeyPressed(D)) XDirection++;
        if (Keyboard.IsKeyPressed(W)) YDirection--;
        if (Keyboard.IsKeyPressed(S)) YDirection++;
        if (XDirection != 0 || YDirection != 0)
            ChangeDirection(ship, XDirection, YDirection, dt);
        Rotate(ship);

        
    }
    private void ChangeDirection(Ship ship, float x, float y, float dt)
    {
        Vector2f oldPos = ship.Position;
        
        Vector2f velocity = new Vector2f(x, y) / MathF.Sqrt(x*x + y*y) * ship.Speed;
        Vector2f newPos = oldPos + velocity * dt;
        ship.Position = newPos;
        if (ship.IsOutOfBounds()) ship.Position = oldPos;
    }
}