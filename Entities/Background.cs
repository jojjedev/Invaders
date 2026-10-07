using SFML.Graphics;
using SFML.System;
using static Invaders.Constants;

namespace Invaders;

public sealed class Background : Entity
{
    private static int yPosition;
    private IntRect _backgroundRect = new IntRect(0, 0, 256, 256);
    private float _timer = 1.2f;

    public Background() : base("darkPurple")
    {
        _sprite.TextureRect = _backgroundRect;
        _sprite.Origin = new Vector2f(128, 128);
    }

    public override void Update(Scene scene, float dt)
    {
        _timer += dt;
        if (_timer >= 1f/144f)
        {
            yPosition++;
            _timer = 0;

            if (yPosition >= 256)
            {
                yPosition = 0;
            }
        }


    }
    public override void Render(RenderTarget target)
    {
        View view = target.GetView();
        int tilesX = (int)MathF.Ceiling(view.Size.X / 256);
        int tilesY = (int)MathF.Ceiling(view.Size.Y / 256);
        for (int row = -1; row <= tilesY; row++)
        {
            for (int col = 0; col <= tilesX; col++)
            { 
                _sprite.Position = 256 * new Vector2f(col, row) + new Vector2f(0, yPosition);
                
                base.Render(target);
            }
        }
    }
}