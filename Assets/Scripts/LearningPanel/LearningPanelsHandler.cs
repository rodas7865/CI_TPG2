using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LearningPanelsHandler : MonoBehaviour
{
    private List<LearningPanel> learningPanels;

    [SerializeField]
    private int currentPanel = 0;

    [SerializeField]
    private float paddingTarget = 0;

    private RectTransform trans;

    void Start()
    {
        trans = GetComponent<RectTransform>();

        learningPanels = new List<LearningPanel>();

        foreach (Transform trans in transform) {

            LearningPanel panel = trans.GetComponent<LearningPanel>();

            if (panel != null)
            {
                learningPanels.Add(panel);

                panel.arrowLeft.GetComponent<Button>().onClick.AddListener(SlideLeft);
                panel.arrowRight.GetComponent<Button>().onClick.AddListener(SlideRight);
            }
                
        }

        

        learningPanels[0].arrowLeft.GetComponent<Button>().onClick.RemoveAllListeners();
        learningPanels[0].arrowLeft.GetComponent<Image>().color = new Color(255,255,255,0);

        learningPanels[learningPanels.Count - 1].arrowRight.GetComponent<Button>().onClick.RemoveAllListeners();
        learningPanels[learningPanels.Count - 1].arrowRight.GetComponent<Image>().color = new Color(255, 255, 255, 0);
    }

    void Update()
    {
        paddingTarget = currentPanel*-1920f;

        Vector2 target = new Vector2(Mathf.Lerp(trans.anchoredPosition.x, paddingTarget, Time.deltaTime * 5f), trans.anchoredPosition.y);

        trans.anchoredPosition = target;
        
    }

    public void SlideLeft()
    {
        if(currentPanel > 0) 
            currentPanel--;
    }

    public void SlideRight()
    {

        if (currentPanel <= learningPanels.Count-1)
            currentPanel++;
    }
}
