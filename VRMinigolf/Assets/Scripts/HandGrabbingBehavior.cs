using UnityEngine;

public class HandGrabbingBehavior : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the object!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClubHandle"))
        {
            if (animator != null)
            {
                animator.SetBool("isGrabbing", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ClubHandle"))
        {
            if (animator != null)
            {
                animator.SetBool("isGrabbing", false);
            }
        }
    }
}
