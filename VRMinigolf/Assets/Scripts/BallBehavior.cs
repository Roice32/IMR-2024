using System;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private static readonly Quaternion RESET_ROTATION = Quaternion.Euler(0, 0, 0);
    private static readonly Vector3 RESET_ROTATION_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_COORDS = new(0, 0.25f, 0);
    private static readonly int HIT_COOLDOWN_MILLIS = 1000;
    
    private GameLogic GameLogic;
    private Rigidbody ControlledRigidBody;

    public int TimesHit { get; private set; }
    private int CooldownMillisLeft;

    void Start()
    {
        ControlledRigidBody = GetComponent<Rigidbody>();
        GameLogic = FindObjectOfType<GameLogic>();
        TimesHit = 0;
        CooldownMillisLeft = 0;
    }

    void Update()
    {
        if (CooldownMillisLeft > 0)
        {
            CooldownMillisLeft -= (int)(Time.deltaTime * 1000);
        }
        if (CooldownMillisLeft < 0)
        {
            CooldownMillisLeft = 0;
        }
    }

    public void Reset()
    {
        ControlledRigidBody.angularVelocity = RESET_ROTATION_VELOCITY;
        transform.rotation = RESET_ROTATION;

        ControlledRigidBody.velocity = RESET_VELOCITY;
        transform.position = RESET_COORDS;

        TimesHit = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Club"))
        {
            if (CooldownMillisLeft == 0)
            {
                CooldownMillisLeft = HIT_COOLDOWN_MILLIS;
                TimesHit++;
                GameLogic.UpdateOnBallHit();
            }
        }
    }
}
