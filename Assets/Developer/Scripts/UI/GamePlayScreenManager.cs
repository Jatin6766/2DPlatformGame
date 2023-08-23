using HefeGames;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayScreenManager : MonoBehaviour
{
    public static GamePlayScreenManager Instance;
    
    [SerializeField] GameObject screenGameOver;

    [Header("GamOver")]
    [SerializeField] TextMeshProUGUI scoretext; 


    private void Awake()
    {
        Instance = this;
    }
    
    public void OnGameOver()
    {
        screenGameOver.SetActive(true);
        scoretext.text = "Score: "+ HUDController.Instance.Score;
    }
    public void Onclcik_GameOver_HomeButton()
    {
        SceneManager.LoadScene(MyKeywords.homeScenename);
        SoundManager.Instance.ClickSound();
    }

    public void Onclcik_GameOver_ReplayButton()
    {
        SceneManager.LoadScene(MyKeywords.gamePlayScenename);
        SoundManager.Instance.ClickSound();
    }
}
