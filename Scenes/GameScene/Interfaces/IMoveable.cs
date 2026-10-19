using SFML.System;

namespace Invaders;

public interface IMoveable
{
    public void Move(float dt);
    public bool IsOutOfBounds();

}