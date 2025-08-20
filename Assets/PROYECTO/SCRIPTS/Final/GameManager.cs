using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
