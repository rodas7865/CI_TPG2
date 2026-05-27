using UnityEngine;

public class BasicAnimatedSceneChange : MonoBehaviour
{

    [SerializeField]
    private Animator mainAnimator;

    [SerializeField]
    private string nameOfExitAnimation;

    private string nextScene;
    private bool playedAnimation;

    public void OpenScene(string sectionSceneName)
    {
        mainAnimator.SetTrigger("PlayExitAnimation");
        nextScene = sectionSceneName;
    }

    void Update()
    {
        AnimatorStateInfo state = mainAnimator.GetCurrentAnimatorStateInfo(0);

        if (state.IsName(nameOfExitAnimation) && state.normalizedTime > 1.0f && !playedAnimation)
        {
            playedAnimation = true;
            SceneHandler.ChangeScene(nextScene);
        }
            
        
    }
}
