using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animationController;

    void Start()
    {
        animationController = GetComponent<Animator>();

        if (animationController != null)
        {
            animationController.SetBool("Idling", true);
        }
        else
        {
            Debug.LogError($"Could not find Animator for Character ID {this}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
