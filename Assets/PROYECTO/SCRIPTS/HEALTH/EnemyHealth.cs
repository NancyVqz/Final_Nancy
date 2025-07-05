using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int dano = 34;
    private int vida = 100;

    private void Update()
    {
        Morir();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            vida -= dano;
        }
    }

    public void Morir()
    {
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }   
    
    public void Dano()
    {
        vida -= dano;
    }

    [ContextMenu("Matar Personaje")]  //aparece un boton para matar al personaje
    public void KillCharacter()
    {
        EnemySpawn spawn = FindObjectOfType<EnemySpawn>();
        spawn.OnCharacterKilled(this.gameObject);
    }
}
