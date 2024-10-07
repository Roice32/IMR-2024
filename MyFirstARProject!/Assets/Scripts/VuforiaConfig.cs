using UnityEngine;
using Vuforia;

public class VuforiaConfig : MonoBehaviour
{
    void Start()
    {
        VuforiaBehaviour.Instance.SetMaximumSimultaneousTrackedImages(2);
        RestartVuforia();
    }

    void RestartVuforia()
    {
        VuforiaBehaviour.Instance.enabled = false;
        VuforiaBehaviour.Instance.enabled = true;
    }
}
