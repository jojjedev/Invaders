using SFML.System;

namespace Invaders;

public interface IMoveable
{
    public Vector2f Position { get; set; }
    public void Move(float dt);
    
    
}