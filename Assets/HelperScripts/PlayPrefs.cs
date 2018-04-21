using UnityEngine;

public static class PlayPrefs
{
    private const string MUSIC_VOLUME_KEY = "MusicVolume";

    public const float DEFAULT_MUSIC_VOLUME = 0.5f;

    public static float MusicVolume
    {
        get { return PlayerPrefs.HasKey(MUSIC_VOLUME_KEY) ? PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY) : DEFAULT_MUSIC_VOLUME; }
        set { PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, Mathf.Clamp(value, 0f, 1f)); }
    }

    private const string GAME_VOLUME_KEY = "GameVolume";

    public const float DEFAULT_GAME_VOLUME = 0.5f;

    public static float GameSoundVolume
    {
        get { return PlayerPrefs.HasKey(GAME_VOLUME_KEY) ? PlayerPrefs.GetFloat(GAME_VOLUME_KEY) : DEFAULT_GAME_VOLUME; }
        set { PlayerPrefs.SetFloat(GAME_VOLUME_KEY, Mathf.Clamp(value, 0f, 1f)); }
    }

    private const string GAME_CONTRAST_KEY = "GameContrast";

    public const float DEFAULT_GAME_CONTRAST = 0.5f;

    public static float GameContrast
    {
        get { return PlayerPrefs.HasKey(GAME_CONTRAST_KEY) ? PlayerPrefs.GetFloat(GAME_CONTRAST_KEY) : DEFAULT_GAME_CONTRAST; }
        set { PlayerPrefs.SetFloat(GAME_CONTRAST_KEY, Mathf.Clamp(value, 0f, 1f)); }
    }
}
