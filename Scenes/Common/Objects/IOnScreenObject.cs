using SFML.Graphics;

namespace Invaders;

public interface IOnScreenObject
{
    public bool Dead { get;}
    public bool DontClearOnLoad { get; set; }
    public void Create(Scene scene);
    public void Destroy(Scene scene);
    public void Update(Scene scene, float dt);
    public void Render(RenderTarget target);
}