using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private static readonly Quaternion RESET_ROTATION = Quaternion.Euler(0, 0, 0);
    private static readonly Vector3 RESET_ROTATION_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_COORDS = new(0, 0.25f, 0);

    private Rigidbody ControlledRigidBody;
    private int TimesHit;

    void Start()
    {
        ControlledRigidBody = GetComponent<Rigidbody>();
        TimesHit = 0;
    }

    void Update()
    {
    }

    public void Reset()
    {
        ControlledRigidBody.angularVelocity = RESET_ROTATION_VELOCITY;
        transform.rotation = RESET_ROTATION;

        ControlledRigidBody.velocity = RESET_VELOCITY;
        transform.position = RESET_COORDS;

        TimesHit = 0;
    }
}
