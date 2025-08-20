using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI texto;
    private int score = 0;

    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
       score = GameManager.instance.score;
       texto.text = "Score: " + score;
       PlayFabHandler playFabHandler  = FindObjectOfType<PlayFabHandler>();
        playFabHandler.score = score;
    }
}
