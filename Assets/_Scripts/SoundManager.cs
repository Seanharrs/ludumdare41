using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioClip[] musicArray;
    
    private AudioSource m_Audio;

    private static SoundManager m_Instance;

    private void Awake()
    {
        if(m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        m_Audio = GetComponent<AudioSource>();
        m_Audio.volume = PlayPrefs.MusicVolume;
        m_Audio.clip = musicArray[0];
        m_Audio.loop = true;
        m_Audio.Play();
    }

    public void SetMusicVolume(float val)
    {
        m_Audio.volume = val;
    }

    public void SetGameSoundVolume(float val)
    {
        foreach(AudioSource obj in FindObjectsOfType<AudioSource>())
        {
            if(obj.gameObject == gameObject)
                continue;

            obj.volume = val;
        }
    }

    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetMusicVolume(PlayPrefs.MusicVolume);
        SetGameSoundVolume(PlayPrefs.GameSoundVolume);
        GameObject mainLight = GameObject.FindGameObjectWithTag("MainLight");
        if(mainLight)
            mainLight.GetComponent<Light>().color = new Color(PlayPrefs.GameContrast, PlayPrefs.GameContrast, PlayPrefs.GameContrast, 1f);

        if(scene.name.Contains("Menu") && m_Audio.clip != musicArray[0])
            PlayMusicAtIndex(0);
        else if(!scene.name.Contains("Menu") && m_Audio.clip != musicArray[1])
            PlayMusicAtIndex(1);
    }

    private void PlayMusicAtIndex(int index)
    {
        m_Audio.clip = musicArray[index];
        m_Audio.loop = true;
        m_Audio.Play();
    }
}
