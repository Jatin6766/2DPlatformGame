using HefeGames;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    AudioSource soundAudioSource;
    [SerializeField] AudioSource musicAudioSource;
    
    public AudioClip click;
    public AudioClip playerHit;
    public AudioClip playerShoot;
    public AudioClip enemyHitBody;
    public AudioClip EnemyHitHead;
    public AudioClip playerDie;
    public AudioClip playerJump;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        soundAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        AudioDefaulfSetting();

        SetMusicValue(PlayerPrefs.GetFloat(MyKeywords.Music));

        PlayMusic();
    }

    public void SetSoundValue(float val)
    {
        soundAudioSource.volume = val;
    }

    public void SetMusicValue(float val)
    {
        musicAudioSource.volume = val;
    }

    void AudioDefaulfSetting()
    {
        if (!PlayerPrefs.HasKey(MyKeywords.Music))
        {
            PlayerPrefs.SetFloat(MyKeywords.Music,1);
        }

        if (!PlayerPrefs.HasKey(MyKeywords.Sound))
        {
            PlayerPrefs.SetInt(MyKeywords.Sound,1);
        }
    }

    public void PlayMusic()
    { 
        musicAudioSource.Play();
    }

    public void ClickSound()
    {
        if (PlayerPrefs.GetInt(MyKeywords.Sound) == 1)
        { 
            soundAudioSource.PlayOneShot(click);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (PlayerPrefs.GetInt(MyKeywords.Sound) == 1)
        {
            soundAudioSource.PlayOneShot(clip);
        }
    }

}
