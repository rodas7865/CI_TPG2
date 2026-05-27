using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicAnimatedSceneChange : MonoBehaviour
{

    [SerializeField]
    private Animator mainAnimator;

    [SerializeField]
    private Animator retreatAnimator;

    [SerializeField]
    private string nameOfExitAnimation;

    private bool inScene = true;
    private bool changingScene = false;

    void Start()
    {
        PlayEntry();
    }

    public void OpenScene(string sectionSceneName)
    {
        if (changingScene || !inScene) 
            return;

        StartCoroutine(ChangeSceneRoutine(sectionSceneName,false));
    }

    public void Retreat()
    {
        if (changingScene || !inScene)
            return;

        StartCoroutine(ChangeSceneRoutine("", true));
    }

    public void PlayEntry()
    {
        changingScene = false;
        inScene = true;

        mainAnimator.ResetTrigger("PlayExitAnimation");
        mainAnimator.SetTrigger("PlayEntryAnimation");

        if (retreatAnimator != null)
        {
            retreatAnimator.ResetTrigger("PlayExitAnimation");
            retreatAnimator.SetTrigger("PlayEntryAnimation");
        } 
    }

    private IEnumerator ChangeSceneRoutine(string sceneName, bool isRetreating)
    {
        changingScene = true;

        mainAnimator.ResetTrigger("PlayEntryAnimation");

        if (retreatAnimator!=null)
        {
            retreatAnimator.ResetTrigger("PlayEntryAnimation");
            retreatAnimator.SetTrigger("PlayExitAnimation");

            yield return WaitForAnimation(retreatAnimator, "ArrowExitAnimation");
        }

        mainAnimator.SetTrigger("PlayExitAnimation");
        yield return WaitForAnimation(mainAnimator, nameOfExitAnimation);

        if (isRetreating)
        {
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
        else
        {
            SceneHandler.ChangeScene(sceneName);
            inScene = false;
        }
    }

    private IEnumerator WaitForAnimation(Animator animator, string stateName)
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            yield return null;

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            yield return null;
    }

    void Update()
    {
        if (inScene == false && SceneManager.GetSceneAt(SceneManager.sceneCount - 1) == gameObject.scene)
            PlayEntry();
    }
}