using SFML.Graphics;

namespace Invaders;

public interface IRenderable
{
    public void Render(RenderTarget target);
}