using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool[] objetos = new bool[8];

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
