using UnityEngine;
using UnityEngine.EventSystems;

public class LearnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Animator buttonAnimator;

    [SerializeField]
    private Animator textAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("OpenBook");
        textAnimator.SetBool("Flashing", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnimator.SetTrigger("CloseBook");
        textAnimator.SetBool("Flashing", false);
    }
}
