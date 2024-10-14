using System;
using UnityEngine;

public class HoleObserver : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (BallEnteredHole(other))
        {
            Debug.Log("Ball entered hole");
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
