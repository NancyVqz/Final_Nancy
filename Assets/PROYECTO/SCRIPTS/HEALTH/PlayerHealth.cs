using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;
    private TextMeshProUGUI texto;
    private int vida = 100;
    private int dano = 25;

    private GameObject player;

    private void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();

        player = GameObject.Find("Player");

        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        texto.text = "Vida: " + vida;

        if (vida <= 0)
        {
            Reaparecer();
        }
    }

    public void RestarVida()
    {
        vida -= dano;
    }

    public void Reaparecer()
    {
        CharacterController controller = player.GetComponent<CharacterController>();

        controller.enabled = false;
        controller.transform.position = new Vector3 (-4.55f, 1.101f, -55.9f);
        AudioManager.instance.Play("Death");
        controller.enabled = true;

        vida = 100;
    }
}
