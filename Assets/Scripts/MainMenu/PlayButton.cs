using UnityEngine;
using UnityEngine.EventSystems;

public class PlayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Animator buttonAnimator;

    [SerializeField]
    private Animator textAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonAnimator.SetBool("Hovered", true);
        textAnimator.SetBool("Flashing", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonAnimator.SetBool("Hovered", false);
        textAnimator.SetBool("Flashing", false);
    }
}
