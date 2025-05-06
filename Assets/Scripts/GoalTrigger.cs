using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private GameObject endGameUI;
    private bool triggered = false;

    void Awake()
    {
        endGameUI = GameObject.FindGameObjectWithTag("UI");
        if (endGameUI != null)
            endGameUI.SetActive(false);
        else
        {
            Debug.Log("no");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        if (endGameUI != null)
            endGameUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
