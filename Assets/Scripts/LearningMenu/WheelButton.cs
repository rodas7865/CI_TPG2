using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WheelButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField]
    private string buttonName;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    private float alphaThreshold = 0.1f;

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaThreshold;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textMesh.text = buttonName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMesh.text = "";
    }
}
