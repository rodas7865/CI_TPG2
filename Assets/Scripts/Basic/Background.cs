using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    [SerializeField]
    public float animationSpeed = 2f;

    [SerializeField]
    private RectTransform backgroundTransform;

    [SerializeField]
    private List<RectTransform> clouds;

    //[SerializeField]
    float targetBackgroundPosition = -200;

    void Start()
    {
        SceneHandler.ChangeScene("TitleScreen");
    }

    // Update is called once per frame
    void Update()
    {
        switch (SceneHandler.currentSceneDepth)
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
    }

    private void spawnCloud(RectTransform cloud)
    {
        cloud.anchoredPosition = new Vector2(2250f, cloud.anchoredPosition.y );//UnityEngine.Random.Range(-800f, -300f)
        cloud.gameObject.SetActive(true);
    }
}
