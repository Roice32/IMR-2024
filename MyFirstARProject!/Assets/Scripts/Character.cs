using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator AnimationController;
    private List<Character> OtherCharacters;

    void Start()
    {
        GetAnimationController();
        DetectOtherCharacters();
        SignalMySpawningToOthers();
    }

    private void GetAnimationController()
    {
        AnimationController = GetComponent<Animator>();

        if (AnimationController != null)
        {
            AnimationController.SetBool("Idling", true);
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

    private void SignalMySpawningToOthers()
    {
        foreach (Character character in OtherCharacters)
        {
            character.RegisterNewCharacterInScene(this);
        }
    }

    public void RegisterNewCharacterInScene(Character newCharacter)
    {
        OtherCharacters.Add(newCharacter);
    }

    void Update()
    {
        Vector3 myPosition = transform.position;
        
        foreach (Character otherCharacter in OtherCharacters)
        {
            Vector3 otherPosition = otherCharacter.transform.position;
            float distance = Vector3.Distance(myPosition, otherPosition);
            
            if (distance <= 0.1f)
            {
                AnimationController.SetBool("Attacking", true);
                AnimationController.SetBool("Idling", false);
                return;
            }
        }

        AnimationController.SetBool("Idling", true);
        AnimationController.SetBool("Attacking", false);
    }
}
