using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Sprite fillHeart, unFillHeart;
    [SerializeField] Transform lifeHerartHolder;
    
    int maxLife = 3;

    int _score;
    int _life;


    public int Score
    {
        get { return _score; }
        set { _score = value;

            scoreText.text = _score.ToString();
        }
    }

    public int PlayerLife
    {
        get { return _life; }
        set {
            _life = value;

            if (_life == maxLife)
            {
                SetIndicator();
            }
            else if (_life >= 0)
            {
                lifeHerartHolder.GetChild(_life).GetComponent<Image>().sprite = unFillHeart;
            }
        }
    }

    void SetIndicator()
    {
        foreach (Transform item in lifeHerartHolder)
        {
            item.GetComponent<Image>().sprite = fillHeart;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerLife = maxLife;
        Score = 0;
    }

}
