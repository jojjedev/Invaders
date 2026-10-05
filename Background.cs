using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public sealed class Background : Entity, IRenderable
{
    public Background() : base("darkPurple")
    {
        _sprite.TextureRect = BackgroundBounds;
        _sprite.Origin = new Vector2f(128, 128);
    }

    
    public void Render()
    {
        throw new NotImplementedException();
    }
}