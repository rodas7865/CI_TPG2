using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private Animator buttons;

    private string nextScene;

    private bool playedAnimation = false;

    public void OpenLearningMenu()
    {
        buttons.SetTrigger("PlayExitAnimation");
        nextScene = "LearningMenu";
    }

    public void OpenGamesMenu()
    {

        buttons.SetTrigger("PlayExitAnimation");
        nextScene = "GamesMenu";
    }

    void Update()
    {
        AnimatorStateInfo state = buttons.GetCurrentAnimatorStateInfo(0);

        if (state.IsName("ButtonsExitAnimation") && state.normalizedTime > 1.0f && !playedAnimation)
        {
            SceneHandler.ChangeScene(nextScene);
            playedAnimation = true;
        }
            
    }
}
