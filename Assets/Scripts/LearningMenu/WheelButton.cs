using UnityEngine;
using UnityEngine.UI;

public class WheelButton : MonoBehaviour
{
    private float alphaThreshold = 0.1f;

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }
}
