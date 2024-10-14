using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private BallBehavior ObservedBall;
    private TextMeshPro CurrHitsText;
    private TextMeshPro PrevHitsText;
    private TextMeshPro ScoreText;
    private int Score;

    void Start()
    {
        ObservedBall = FindObjectOfType<BallBehavior>();
        CurrHitsText = GameObject.Find("CurrHitsCount").GetComponent<TextMeshPro>();
        PrevHitsText = GameObject.Find("PrevHitsCount").GetComponent<TextMeshPro>();
        ScoreText = GameObject.Find("Score").GetComponent<TextMeshPro>();

        Score = 0;
    }

    public void UpdateOnBallHit()
    {
        CurrHitsText.text = ObservedBall.TimesHit.ToString();
    }

    public void UpdateOnHole()
    {
        Score += GetScoreForHits(ObservedBall.TimesHit);
        ScoreText.text = Score.ToString();
        PrevHitsText.text = ObservedBall.TimesHit.ToString();
        CurrHitsText.text = "0";
    }

    private int GetScoreForHits(int hitsCount)
    {
        return hitsCount switch
        {
            1 => 20,
            2 => 10,
            3 => 8,
            4 => 5,
            5 => 3,
            6 => 2,
            _ => 1,
        };
    }
}
