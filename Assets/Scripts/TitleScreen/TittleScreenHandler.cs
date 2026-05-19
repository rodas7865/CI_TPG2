using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TittleScreenHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text clickHereTxt;

    [SerializeField]
    private Animator titleAnimator;

    private bool playedAnimation = false;

    public void StartApp()
    {
        clickHereTxt.GameObject().SetActive(false);
        titleAnimator.SetTrigger("PlayExitAnimation");
    }

    void Update()
    {
        AnimatorStateInfo state = titleAnimator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("TitleScreenExit") && state.normalizedTime > 1.0f && !playedAnimation)
        {
            playedAnimation = true;
            SceneHandler.ChangeScene("MainMenu");
        } 
    }
}
