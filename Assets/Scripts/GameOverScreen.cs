using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalText;
    public AudioSource goodScoreAudio;
    public AudioSource badScoreAudio;

    void Start()
    {
        goodScoreAudio = gameObject.transform.Find("goodScoreAudio").GetComponent<AudioSource>();
        badScoreAudio = gameObject.transform.Find("badScoreAudio").GetComponent<AudioSource>();
    }

    public void Setup(int score, int maxLevel)
    {


        float scorePercentage = (float)score / maxLevel * 100;

        if (scorePercentage < 20)
        {
            badScoreAudio.Play();
            finalText.text = "HORROROSO";
        }
        else if (scorePercentage < 60)
        {
            badScoreAudio.Play();
            finalText.text = "PESSIMO";
        }
        else if (scorePercentage < 70)
        {
            goodScoreAudio.Play();
            finalText.text = "MUITO BEM";
        }
        else if (scorePercentage < 80)
        {
            goodScoreAudio.Play();
            finalText.text = "EXCELENTE";
        }
        else
        {
            goodScoreAudio.Play();
            finalText.text = "PERFEITO";
        }

        scoreText.text = score.ToString() + " PONTOS";
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
