using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Rounnds Parameters")]
    [SerializeField] private int round = 1;                 //Conteo de ronda
    [SerializeField] private int characterPerRound = 6;     //cantidad maxima de enemigos por ronda
    [SerializeField] private int charSpawnedPerRound = 0;   //Revisa cuantos eneigos spawnearon en esa ronda
    [SerializeField] private int charKilledPerRound = 0;    //cuantos has matado

    [Header("Spawn Parameters")]
    [SerializeField] private int maxCharacterInstanceInQueue; //Maximo de enemigos instanceados en la fila
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject characterToSpawn;   //el personaje  enemigo a spawnear
    [SerializeField] private int maxCharacterCountInScene;  //maximo de personajes ACTIVOS en la escena
    [SerializeField] private int charactersInScene = 0;     //cuantos hay en la escena

    [SerializeField] private Transform[] spawnPoints;       //los spawnpoints
    [SerializeField] Queue<GameObject> characterQueue;

    [Header("SpawnPoints Variables")]
    [SerializeField] private SpawnPoints roundSpawnsData;
    private Transform[] currentRoundSP;

    [Header("Sistema Llaves")]
    [SerializeField] private GameObject[] keys;
    private int indexKey = 0;

    private void Start()
    {
        StartPool();
        keys[indexKey].SetActive(true);
    }

    private void StartPool()
    {
        characterQueue = new Queue<GameObject>(); 

        for (int i = 0; i < maxCharacterInstanceInQueue; i++)
        {
            GameObject instance = Instantiate(characterToSpawn); 
            instance.SetActive(false); 
            characterQueue.Enqueue(instance); 
        }

        StartCoroutine(SpawnCharacters());
    }

    private IEnumerator SpawnCharacters()
    {
        yield return new WaitUntil(() => charactersInScene < maxCharacterCountInScene && charSpawnedPerRound < characterPerRound);

        for (int i = charactersInScene; i < maxCharacterCountInScene; i++)
        {
            if (charSpawnedPerRound >= characterPerRound)
            {
                break;
            }

            yield return new WaitForSeconds(spawnRate);
            GameObject character = characterQueue.Dequeue(); 
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
            indexKey++;
            characterPerRound = Random.Range(8, 13);
            charSpawnedPerRound = 0;

            keys[indexKey].SetActive(true);
            UpdateSpawnPoints();
        }
    }

    private void UpdateSpawnPoints()
    {
        if(round > 10)
        {
            StopAllCoroutines();
        }

        currentRoundSP = roundSpawnsData.SpawnPointRounds(round);

        if(currentRoundSP != null && currentRoundSP.Length > 0)
        {
            spawnPoints = currentRoundSP;
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
