using UnityEngine;

public class HoleObserver : MonoBehaviour
{
    private GameLogic GameLogic;

    private void Start()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BallEnteredHole(other))
        {
            GameLogic.UpdateOnHole();
            ResetBallPosition();
        }
    }

    private bool BallEnteredHole(Collider other)
    {
        return other.CompareTag("Ball");
    }

    private void ResetBallPosition()
    {
        BallBehavior ballContainer = FindObjectOfType<BallBehavior>();
        if (ballContainer != null)
        {
            ballContainer.Reset();
        }
        else
        {
            Debug.LogError("BallContainer not found");
        }
    }
}
