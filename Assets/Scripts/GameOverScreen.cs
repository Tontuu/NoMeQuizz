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

    public void Setup(int score, int maxLevel)
    {
        float scorePercentage = (float)score / maxLevel * 100;

        if (scorePercentage < 20)
        {
            finalText.text = "HORROROSO";
        }
        else if (scorePercentage < 60)
        {
            finalText.text = "PESSIMO";
        }
        else if (scorePercentage < 70)
        {
            finalText.text = "MUITO BEM";
        }
        else if (scorePercentage < 80)
        {
            finalText.text = "EXCELENTE";
        }
        else
        {
            finalText.text = "PERFEITO";
        }

        scoreText.text = score.ToString() + " PONTOS";
        gameObject.SetActive(true);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
