using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    private int currentQuestion = 0;
    private static int score = 0;

    // Text Objects
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI FinalScore;
    public TextMeshProUGUI levelText;

    // Game objects
    public GameOverScreen gameOverScreen;
    public Image questionImage;
    public GameObject GameFinished;
    public Button[] replyButtons;
    public GameObject RightPanel;
    public GameObject WrongPanel;
    public QtsData qtsData;

    // Start is called before the first frame update
    void Start()
    {
        SetQuestion(currentQuestion);
        RightPanel.gameObject.SetActive(false);
        WrongPanel.gameObject.SetActive(false);
        GameFinished.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetQuestion(int questionIndex)
    {
        string levelStr = (questionIndex + 1).ToString().PadLeft(2, '0') + '/'
                            + qtsData.questions.Length.ToString().PadLeft(2, '0');
        levelText.text = levelStr;
        questionText.text = qtsData.questions[questionIndex].questionText;
        questionImage.sprite = qtsData.questions[questionIndex].questionImage;

        foreach (Button r in replyButtons)
        {
            r.onClick.RemoveAllListeners();
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].GetComponentsInChildren<TextMeshProUGUI>()[1].text = qtsData.questions[questionIndex].replies[i];
            int replyIndex = i;
            replyButtons[i].onClick.AddListener(() =>
            {
                CheckReply(replyIndex);
            });
        }
    }


    void CheckReply(int replyIndex)
    {
        int correctIndex = qtsData.questions[currentQuestion].correctReplyIndex;

        Debug.Log("Current question: " + currentQuestion);
        Debug.Log("Correct index: " + correctIndex);

        if (replyIndex == correctIndex)
        {
            score++;
            scoreText.text = "Score - " + score;
            RightPanel.gameObject.SetActive(true);

            // Set color to green
            replyButtons[replyIndex].GetComponentsInChildren<TextMeshProUGUI>()[1].color =
                    new Color(30f / 255f, 15f / 255f, 15f / 69f, 255f / 255f); ;
            ColorBlock cb = replyButtons[replyIndex].colors;
            cb.selectedColor = new Color((54f / 255f), (241f / 255f), (190f / 255f));
            replyButtons[replyIndex].colors = cb;
        }
        else
        {
            Debug.Log(replyButtons[replyIndex].colors.selectedColor);
            WrongPanel.gameObject.SetActive(true);

            // Change button color and highlight the right answer.
            ColorBlock cb = replyButtons[replyIndex].colors;
            replyButtons[replyIndex].GetComponentsInChildren<TextMeshProUGUI>()[1].color =
                    new Color(30f / 255f, 15f / 255f, 15f / 69f, 255f / 255f);
            cb.selectedColor = new Color((233f / 255f), (55f / 255f), (68f / 255f));
            replyButtons[replyIndex].colors = cb;

            // Highlight correct answer
            cb.disabledColor = new Color((54f / 255f), (241f / 255f), (190f / 255f), 0.5f);
            replyButtons[correctIndex].colors = cb;
            replyButtons[correctIndex].GetComponentsInChildren<TextMeshProUGUI>()[1].color =
                    new Color(30f / 255f, 15f / 255f, 15f / 69f, 255f / 255f); ;
        }

        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i != replyIndex)
            {
                replyButtons[i].interactable = false;
            }
        }
        StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(2);
        currentQuestion++;
        if (currentQuestion < qtsData.questions.Length)
        {
            Reset();
        }
        else
        {
            gameOverScreen.Setup(score, qtsData.questions.Length);
            score = 0;
        }
    }
    public void Reset()
    {
        RightPanel.SetActive(false);
        WrongPanel.SetActive(false);

        foreach (Button r in replyButtons)
        {
            ColorBlock cb = r.colors;
            cb.disabledColor = new Color((25f / 255f), (13f / 255f), (58f / 255f), 1f);
            r.colors = cb;
            r.GetComponentsInChildren<TextMeshProUGUI>()[1].color = Color.white;
            r.interactable = true;
        }
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        SetQuestion(currentQuestion);
    }

}
