using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionData", menuName = "QuestionData")]
public class QtsData : ScriptableObject
{
    [System.Serializable]
    public struct Question
    {
        public string questionText;
        public string[] replies;
        public int correctReplyIndex;
        public Sprite questionImage;
    }

    public Question[] questions;


    public void Reshuffle()
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia :)
        for (int i = 0; i < questions.Length; i++)
        {
            // Shuffle questions
            Question tmp = questions[i];
            int r = Random.Range(i, questions.Length);
            questions[i] = questions[r];
            questions[r] = tmp;
        }
    }
}
