using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* EJERCICIO-
 * 1.- La cantidad de personajes que van a aparecer debe estar definida por la variable characterPerRound, sin embargo la cantidad de personajes que puede haber en la escena 
 * a la ves, esta limitada por la variable maxCharacterCountInScene
 * 
 * 2.- Cuando mates a un enemigo, si aun quedan por personajes por aparecer durante esa ronda, que sigan apareciendo hasta llegar al limite definido por characterPerRound;
 * 
 * 3.- Una ves hayan muerto la cantidad de personajes marcados en characetrPerRound, deben de iniciar otra ronda, cada ves sumandole 2 personajes mas.
 * 
 * 4.- La primer ronda debe de iniciar en 6 personajes. 
 */
public class Spawn : MonoBehaviour
{
    [SerializeField] private int characterPerRound = 6; //indica la cantidad maxima de enemigos por ronda, es decir, para que acabe la ronda se debe de matar esta cantidad de enemigos
    [SerializeField] private int round = 1;
    [SerializeField] private int charKilledPerRound = 0;
    [SerializeField] private int charSpawnedPerRound = 0; //Te revisa cuantos personajes ya spawneaste de el total que habra en la ronda

    [SerializeField] private GameObject characterToSpawn; //el personaje  enemigo a spawnear
    [SerializeField] private Transform[] spawnPoints; //los spawnpoints o puntos de transform donde aparece

    [SerializeField] private int maxCharacterCountInScene; //Maximo de personajes ACTIVOS en la escena
    [SerializeField] private int maxCharacterInstanceInQueue; //Maximo de personajes DISPONIBLES para usar

    [SerializeField] private float spawnRate;

    [SerializeField] Queue<GameObject> characterQueue;
    [SerializeField] private int charactersInScene = 0;

    //[SerializeField] private float characterLifeTime; //variable de prueba para que los personajes muerieran cada x tiempo

    private void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        characterQueue = new Queue<GameObject>(); //se inicia la fila

        for (int i = 0; i < maxCharacterInstanceInQueue; i++)
        {
            GameObject instance = Instantiate(characterToSpawn); //los instancia
            instance.SetActive(false); //desactiva
            characterQueue.Enqueue(instance); //se agrega a la fila
        }

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        yield return new WaitUntil(() => charactersInScene < maxCharacterCountInScene && charSpawnedPerRound < characterPerRound);

        for (int i = charactersInScene; i < maxCharacterCountInScene; i++)
        {
            if (charSpawnedPerRound > characterPerRound)
            {
                break;
            }
            yield return new WaitForSeconds(spawnRate);
            GameObject character = characterQueue.Dequeue(); //sacamos a uno de la fila y lo guardamos en una variable temporal
            character.SetActive(true);
            int randomSpawn = RandomSpawnPoint();
            character.transform.position = spawnPoints[randomSpawn].position;
            character.transform.rotation = spawnPoints[randomSpawn].rotation;
            charactersInScene++;
            charSpawnedPerRound++;
        }
        StartCoroutine(SpawnCharacters());
    }

    private void RoundUpdate()
    {
        if (NewRound())
        {
            charKilledPerRound = 0;
            round++;
            characterPerRound += 2;
            charSpawnedPerRound = 0;
        }
    }

    private bool NewRound()
    {
        return charKilledPerRound == characterPerRound;
    }

    private int RandomSpawnPoint()
    {
        return Random.Range(0, spawnPoints.Length);
    }

    public void OnCharacterKilled(GameObject killedCharacter)
    {
        killedCharacter.SetActive(false);
        characterQueue.Enqueue(killedCharacter);
        charactersInScene--;
        charKilledPerRound++;
        RoundUpdate();
    }
}
