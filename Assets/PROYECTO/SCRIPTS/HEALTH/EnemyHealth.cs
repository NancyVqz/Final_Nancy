using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    //private int dano = 100;
    //private int vida = 100;

    //private void Update()
    //{
    //    Morir();
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            KillCharacter();
        }
    }

    //public void Morir()
    //{
    //    if (vida <= 0)
    //    {
    //        KillCharacter();
    //    }
    //}   
    
    //public void Dano()
    //{
    //    vida -= dano;
    //}

    [ContextMenu("Matar Personaje")]  //aparece un boton para matar al personaje
    public void KillCharacter()
    {
        EnemySpawn spawn = FindObjectOfType<EnemySpawn>();
        spawn.OnCharacterKilled(this.gameObject);
    }
}
