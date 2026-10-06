using SFML.Graphics;

namespace Invaders;
// Från Inlämning 4, Pacman. Gjord tillsammans med Gustav Hjertsson.
public class AssetManager
{
    private readonly string _assetPath = "assets";
    private readonly Dictionary<string, Texture> _textures;
    private readonly Dictionary<string, Font> _fonts;


    public AssetManager()
    {
        _textures = new Dictionary<string, Texture>();
        _fonts = new Dictionary<string, Font>();
    }
    public Texture LoadTexture(string name)
    {
        if (_textures.TryGetValue(name, out Texture found)) return found;
        Texture texture = new Texture($"{_assetPath}/{name}.png");
        _textures.Add(name,texture);
        return texture;
    }

    public Font LoadFont(string name)
    {
        if (_fonts.TryGetValue(name, out Font found)) return found;
        Font font = new Font($"{_assetPath}/{name}.ttf");
        _fonts.Add(name, font);
        return font;
    }
}