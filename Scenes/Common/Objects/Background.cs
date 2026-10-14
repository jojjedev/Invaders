using SFML.Graphics;
using SFML.System;

namespace Invaders;

public sealed class Background : Entity
{
    private static int _yPosition;
    private readonly IntRect _backgroundRect = new IntRect(0, 0, 256, 256);
    private float _timer = 1.2f;

    public Background() : base("darkPurple")
    {
        Sprite.TextureRect = _backgroundRect;
        Sprite.Origin = new Vector2f(128, 128);
        DontClearOnLoad = true;
    }
    public void SetColor(Color color)
    {
        Sprite.Color = color;
    }
    public override void Create(Scene scene)
    {
        Sprite.Texture = scene.Assets.LoadTexture(TextureName);
    }
    public override void Update(Scene scene, float dt)
    {
        _timer += dt;
        if (_timer >= 1f/144f)
        {
            _yPosition++;
            _timer = 0;
            if (_yPosition >= 256) _yPosition = 0;
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
                Sprite.Position = 256 * new Vector2f(col, row) + new Vector2f(0, _yPosition);
                base.Render(target);
            }
        }
    }
}