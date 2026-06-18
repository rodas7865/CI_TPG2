using UnityEngine;
using UnityEngine.SceneManagement;

public class LearningMenu : MonoBehaviour
{
    [SerializeField]
    private Animator wheel;

    private string nextScene;

    private bool playedAnimation = false;

    public void OpenLearningSection(string sectionSceneName)
    {
        wheel.SetTrigger("PlayExitAnimation");
        nextScene = sectionSceneName;
    }

    void Update()
    {
        AnimatorStateInfo state = wheel.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("ButtonsExitAnimation") && state.normalizedTime > 1.0f && !playedAnimation)
        {
            playedAnimation = true;
            SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        }
            
        
    }
}
