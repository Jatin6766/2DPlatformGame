using HefeGames;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeScreenManager : MonoBehaviour
{
    public static HomeScreenManager Instance;

    [SerializeField] GameObject screenOptions;

    [Header("Option Screen")]
    [SerializeField] Transform soundObj;
    [SerializeField] Slider musicController;
    

    private void Awake()
    {
        Instance = this;
    }
    
    public void OnClick_HomeScreen_PlayButton()
    {
        SceneManager.LoadScene(MyKeywords.gamePlayScenename);
        SoundManager.Instance.ClickSound();
    }

    public void OnClick_HomeScreen_Options()
    {
        SoundManager.Instance.ClickSound();
        screenOptions.SetActive(true);
        LoadSettings();
    }

    public void Onclcik_Option_back()
    {
        SoundManager.Instance.ClickSound();
        screenOptions.SetActive(false);
    }

    public void OnClick_Option_Sound()
    {
        if (PlayerPrefs.GetInt(MyKeywords.Sound) == 0)
        {
            PlayerPrefs.SetInt(MyKeywords.Sound, 1);
            soundObj.GetChild(1).gameObject.SetActive(false);
            soundObj.GetChild(0).gameObject.SetActive(true);
        }
        else
        { 
            PlayerPrefs.SetInt(MyKeywords.Sound, 0);
            soundObj.GetChild(0).gameObject.SetActive(false);
            soundObj.GetChild(1).gameObject.SetActive(true);
        }
        SoundManager.Instance.ClickSound();
    }

    public void OnChange_Option_Music()
    {
        SoundManager.Instance.SetMusicValue(musicController.value);
        PlayerPrefs.SetFloat(MyKeywords.Music, musicController.value);
    }

    void LoadSettings()
    {
        musicController.value = PlayerPrefs.GetFloat(MyKeywords.Music);

        if (PlayerPrefs.GetInt(MyKeywords.Sound) == 1)
        {
            soundObj.GetChild(1).gameObject.SetActive(false);
            soundObj.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            soundObj.GetChild(0).gameObject.SetActive(false);
            soundObj.GetChild(1).gameObject.SetActive(true);
        }
    }

}
