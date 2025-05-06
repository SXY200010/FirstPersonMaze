using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorialPanel;

    private bool tutorialShown = true;

    void Start()
    {
        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }

    void Update()
    {
        if (tutorialShown && Input.anyKeyDown)
        {
            tutorialShown = false;

            if (tutorialPanel != null)
                tutorialPanel.SetActive(false);
        }
    }
}
