using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static readonly double ATTACK_RANGE = 0.15d;

    private Animator AnimationController;
    private List<Character> OtherCharacters;
    private Vector3 MyPosition;


    private void GetAnimationController()
    {

        if (TryGetComponent(out AnimationController))
        {
            GoIdle();
        }
        else
        {
            Debug.LogError($"Could not find Animator for Character ID {this}");
        }
    }

    private void DetectOtherCharacters()
    {
        OtherCharacters = FindObjectsOfType<Character>().ToList();
        OtherCharacters.Remove(this);
    }

    private void GoIdle()
    {
        AnimationController.SetBool("Idling", true);
        AnimationController.SetBool("Attacking", false);
    }

    private void StartAttacking()
    {
        AnimationController.SetBool("Attacking", true);
        AnimationController.SetBool("Idling", false);
    }

    private double GetDistanceToCharacter(Character otherCharacter)
    {
        Vector3 otherPosition = otherCharacter.transform.position;
        return Vector3.Distance(MyPosition, otherPosition);
    }

    void Start()
    {
        GetAnimationController();
        DetectOtherCharacters();
    }

    void Update()
    {
        MyPosition = transform.position;

        foreach (Character otherCharacter in OtherCharacters)
        {
            if (GetDistanceToCharacter(otherCharacter) <= ATTACK_RANGE)
            {
                StartAttacking();
                return;
            }
        }

        GoIdle();
    }
}
