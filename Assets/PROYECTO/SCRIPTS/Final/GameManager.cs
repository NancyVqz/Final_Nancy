using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool[] objetos = new bool[9];

    public bool[] keyID = new bool[9];

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
