using System.Collections;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private static readonly Quaternion RESET_ROTATION = Quaternion.Euler(0, 0, 0);
    private static readonly Vector3 RESET_ROTATION_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_VELOCITY = new(0, 0, 0);
    private static readonly Vector3 RESET_COORDS = new(0, 0.25f, 0);
    private static readonly int HIT_COOLDOWN_MILLIS = 1000;

    private static readonly int HIT_VFX_DURATION_MILLIS = 500;
    private static readonly Vector3 INITIAL_VFX_SIZE = new(0.01f, 0.01f, 0.01f);
    private static readonly Vector3 FINAL_VFX_SIZE = new(0.3f, 0.3f, 0.3f);

    private GameLogic GameLogic;
    private Rigidbody ControlledRigidBody;
    public GameObject HitVFXPrefab;

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
            HandleHitByClub(collision);
        }
    }

    private void HandleHitByClub(Collision collision)
    {
        if (CooldownMillisLeft > 0)
        {
            return;
        }
        
        CooldownMillisLeft = HIT_COOLDOWN_MILLIS;
        TimesHit++;
        GameLogic.UpdateOnBallHit();
        TriggerHitVFX(collision.contacts[0].point);
    }

    private void TriggerHitVFX(Vector3 coords)
    {
        GameObject hitVFXInstance = Instantiate(HitVFXPrefab, coords, Quaternion.identity);
        StartCoroutine(HitVFXAnimation(hitVFXInstance));
    }

    private IEnumerator HitVFXAnimation(GameObject hitVFXInstance)
    {
        float timePassed = 0;
        while (timePassed < HIT_VFX_DURATION_MILLIS)
        {
            hitVFXInstance.transform.localScale = Vector3.Lerp(INITIAL_VFX_SIZE, FINAL_VFX_SIZE, timePassed / HIT_VFX_DURATION_MILLIS);
            timePassed += Time.deltaTime * 1000;
            yield return null;
        }
        Destroy(hitVFXInstance);
    }

}
