using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    public float animationSpeed = 2f;

    [SerializeField]
    public Image backgroundDecorator;

    [SerializeField]
    private RectTransform backgroundTransform;

    [SerializeField]
    private List<RectTransform> clouds;

    [SerializeField]
    private List<string> sceneNamePair;

    [SerializeField]
    private List<Sprite> spritePair;

    float targetBackgroundPosition = -200;

    void Start()
    {
        SceneManager.LoadSceneAsync("TitleScreen", LoadSceneMode.Additive);
    }

    void Update()
    {
        int sceneDepth = 1;

        if(SceneManager.sceneCount>3)
            sceneDepth = (SceneManager.sceneCount) - 3;


        switch (sceneDepth)
        {
            case 1:
                targetBackgroundPosition = -200;
                break;
            case 2:
                targetBackgroundPosition = -0;
                break;
            case 3:
                targetBackgroundPosition = 200;
                break;
        }

        if (Mathf.Abs(backgroundTransform.position.y) - Mathf.Abs(targetBackgroundPosition) <= 10f)
            backgroundTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, targetBackgroundPosition);
        else if (backgroundTransform.position.y != targetBackgroundPosition)
            backgroundTransform.anchoredPosition = new Vector2(backgroundTransform.anchoredPosition.x, Mathf.Lerp(backgroundTransform.anchoredPosition.y, targetBackgroundPosition, Time.deltaTime * animationSpeed));


        foreach (RectTransform item in clouds)
        {
            if (item.anchoredPosition.x <= -400f)
                spawnCloud(item);
            else
                item.anchoredPosition = new Vector2(item.anchoredPosition.x-0.3f, item.anchoredPosition.y);
        }

        for (int i = 0; i < sceneNamePair.Count; i++)
        {
            Scene scene = SceneManager.GetSceneByName(sceneNamePair[i]);

            if (scene.name != null)
            {
                backgroundDecorator.sprite = spritePair[i];
            }
        }

    }

    private void spawnCloud(RectTransform cloud)
    {
        cloud.anchoredPosition = new Vector2(2250f, cloud.anchoredPosition.y );//UnityEngine.Random.Range(-800f, -300f)
        cloud.gameObject.SetActive(true);
    }
}
