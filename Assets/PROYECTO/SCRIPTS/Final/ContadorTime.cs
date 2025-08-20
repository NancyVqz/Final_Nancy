using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ContadorTime : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 10;
    public TextMeshProUGUI timeUI;
    [SerializeField] private UnityEvent OnTimeUp;

    public void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timeUI.text = "" + Mathf.RoundToInt(timeRemaining);

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timeUI.text = "0";
                TimeUp();
            }

        }
    }

    public void TimeUp()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnTimeUp.Invoke();
        Debug.Log("Time´s up");
    }

    public void Restart()
    {
        timeRemaining = 10;
        GameManager.instance.score = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ExtraTime()
    {
        timeRemaining += 5;
    }
}
