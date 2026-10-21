using SFML.Audio;
using SFML.Graphics;

namespace Invaders;
// Från Inlämning 4, Pacman. Gjord tillsammans med Gustav Hjertsson.
public sealed class AssetManager
{
    private readonly string _assetPath = "assets";
    private readonly Dictionary<string, Texture> _textures;
    private readonly Dictionary<string, Font> _fonts;
    private readonly Dictionary<string, SoundBuffer> _sounds;

    public AssetManager()
    {
        _textures = new Dictionary<string, Texture>();
        _fonts = new Dictionary<string, Font>();
        _sounds = new Dictionary<string, SoundBuffer>();
    }
    public Texture LoadTexture(string name)
    {
        if (_textures.TryGetValue(name, out Texture? found)) return found;
        Texture texture = new Texture($"{_assetPath}/{name}.png");
        _textures.Add(name,texture);
        return texture;
    }

    public Font LoadFont(string name)
    {
        if (_fonts.TryGetValue(name, out Font? found)) return found;
        Font font = new Font($"{_assetPath}/{name}.ttf");
        _fonts.Add(name, font);
        return font;
    }

    public SoundBuffer LoadAudio(string name)
    {
        if (_sounds.TryGetValue(name, out SoundBuffer found)) return found;
        SoundBuffer sound = new SoundBuffer($"{_assetPath}/{name}.ogg");
        _sounds.Add(name, sound);
        return sound;
    }
}