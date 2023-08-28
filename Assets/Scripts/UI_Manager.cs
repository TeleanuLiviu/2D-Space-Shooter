using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesprites;
    [SerializeField]
    private Image _imglives;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartText;
    private GameManager _gameManager;
    [SerializeField]
    public Text AmmoCount;
    [SerializeField]
    public GameObject PressC;
    // Start is called before the first frame update
    void Start()
    {
        
        _scoreText.text = "Score:" + 0;
        AmmoCount.text = "Ammo Count:" + 15 + "/" + "15";
        gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(TextCoroutineBoss());
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString(); 
    }

    public void UpdateAmmo(int Ammo)
    {
        AmmoCount.text = "AmmoCount:" + Ammo.ToString()+ "/" + "15";
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives<=3)
        {
            _imglives.sprite = _livesprites[currentLives];
        }
        
        
        if(currentLives == 0)
        {
            gameOverSeq();


        }
    }

    IEnumerator TextCoroutineBoss()
    {
        while (true)
        {
            PressC.SetActive(true);
            PressC.GetComponent<Text>().text = "Press 'C' to collect all powerups";
            yield return new WaitForSeconds(1.0f);
            PressC.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.0f);
            PressC.SetActive(false);
            yield break;
        }


    }

    void gameOverSeq()
    {
        _gameManager.GameOver();
        gameOverText.gameObject.SetActive(true);
        restartText.gameObject.SetActive(true);

        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        

    }
}
