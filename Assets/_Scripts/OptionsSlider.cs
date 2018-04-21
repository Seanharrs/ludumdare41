using UnityEngine;
using UnityEngine.UI;

public class OptionsSlider : MonoBehaviour
{
    private enum Option { MusicVolume, GameSoundVolume, GameContrast }
    [SerializeField] private Option option;

    private Slider slider;

    private SoundManager soundManager;

    private void OnEnable()
    {
        soundManager = FindObjectOfType<SoundManager>();

        slider = GetComponent<Slider>();
        switch(option)
        {
            case Option.MusicVolume:
                option = Option.MusicVolume;
                slider.value = PlayPrefs.MusicVolume;
                break;
            case Option.GameSoundVolume:
                option = Option.GameSoundVolume;
                slider.value = PlayPrefs.GameSoundVolume;
                break;
            case Option.GameContrast:
                option = Option.GameContrast;
                slider.value = PlayPrefs.GameContrast;
                break;
        }
    }

    public void DefaultSettings()
    {
        foreach(Slider s in FindObjectsOfType<Slider>())
        {
            switch(s.GetComponent<OptionsSlider>().option)
            {
                case Option.MusicVolume:
                    s.value = PlayPrefs.DEFAULT_MUSIC_VOLUME;
                    break;
                case Option.GameSoundVolume:
                    s.value = PlayPrefs.DEFAULT_GAME_VOLUME;
                    break;
                case Option.GameContrast:
                    s.value = PlayPrefs.DEFAULT_GAME_CONTRAST;
                    break;
            }
        }
    }

    public void SetMusicVolume()
    {
        PlayPrefs.MusicVolume = slider.value;
        soundManager.SetMusicVolume(PlayPrefs.MusicVolume);
    }

    public void SetGameSoundVolume()
    {
        PlayPrefs.GameSoundVolume = slider.value;
        soundManager.SetGameSoundVolume(PlayPrefs.GameSoundVolume);
    }

    public void SetContrast()
    {
        float val = slider.value;
        PlayPrefs.GameContrast = val;
        GameObject mainLight = GameObject.FindGameObjectWithTag("MainLight");
        if(mainLight)
            mainLight.GetComponent<Light>().color = new Color(val, val, val, 1f);
    }
}
