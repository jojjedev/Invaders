using SFML.Audio;
using SFML.Graphics;

namespace Invaders;
// Från Inlämning 4, Pacman. Gjord tillsammans med Gustav Hjertsson.
public sealed class AssetManager
{
    private readonly string _assetPath = "assets";
    private readonly Dictionary<string, Texture> _textures;
    private readonly Dictionary<string, Font> _fonts;
    private readonly Dictionary<SoundBuffer, Sound> _sounds;
    private readonly Dictionary<string, SoundBuffer> _buffers;
    private readonly List<string> _availableBuffers;
    public AssetManager()
    {
        _textures = new Dictionary<string, Texture>();
        _fonts = new Dictionary<string, Font>();
        _buffers = new Dictionary<string, SoundBuffer>();
        _sounds = new Dictionary<SoundBuffer, Sound>();
        _availableBuffers = new List<string>()
        {
            "explosionCrunch_000",
            "sfx_twoTone",
            "select_002",
            "select_005",
            "select_006",
            "menu",
            "battle",
            "laserSmall_000"
        };
        LoadAllGameAudio();
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

    public Sound LoadAudio(string name)
    {
        if (_buffers.TryGetValue(name, out SoundBuffer found)) return _sounds[found];
        SoundBuffer buffer = new SoundBuffer($"{_assetPath}/{name}.ogg");
        Sound sound = new Sound(buffer);
        _buffers.Add(name, buffer);
        _sounds.Add(buffer, sound);
        
        return sound;
    }

    private void LoadAllGameAudio()
    {
        foreach (string bufferName in _availableBuffers)
        {
            SoundBuffer buffer = new SoundBuffer($"{_assetPath}/{bufferName}.ogg");
            Sound sound = new Sound(buffer);
            _buffers.Add(bufferName, buffer);
            _sounds.Add(buffer,sound);
        }
    }

    public Sound PlaySound(string name, int vol, bool loop)
    {
        Sound sound = LoadAudio(name);
        sound.Volume = vol;
        sound.Loop = loop;
        sound.Play();
        return sound;
    }
    
}