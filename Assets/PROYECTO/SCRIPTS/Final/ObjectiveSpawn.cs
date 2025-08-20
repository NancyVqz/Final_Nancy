using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveSpawn : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private GameObject area;
    [SerializeField] private GameObject prefabObjetivo;
    [SerializeField] private GameObject prefabExtra;

    [SerializeField] Queue<GameObject> objetivoQueue;  
    [SerializeField] private int objectiveInScene = 0;  //cuantos hay en escena
    [SerializeField] private int maxCharacterCountInScene = 1; //Maximo de personajes ACTIVOS en la escena
    [SerializeField] private int maxObjectivesInstanceInQueue = 5; //Maximo de personajes DISPONIBLES en fila

    private void OnEnable()
    {
        StartPool();
        StartCoroutine(SpawnExtra());
    }

    private void StartPool()
    {
        objetivoQueue = new Queue<GameObject>(); //se inicia la fila

        for (int i = 0; i < maxObjectivesInstanceInQueue; i++)
        {
            GameObject instance = Instantiate(prefabObjetivo); //los instancia
            instance.SetActive(false); //desactiva
            objetivoQueue.Enqueue(instance); //se agrega a la fila
        }

        StartCoroutine(SpawnObjectives());
    }
    private IEnumerator SpawnObjectives()
    {
        yield return new WaitUntil(() => objectiveInScene < maxCharacterCountInScene);

        for (int i = objectiveInScene; i < maxCharacterCountInScene; i++)
        {
            yield return new WaitForSeconds(0);
            GameObject objetivo = objetivoQueue.Dequeue(); 
            objetivo.SetActive(true);
            
            Vector3 randomSpawn = GetRandomSpawn(); //donde spawnea
            objetivo.transform.position = randomSpawn;
            objetivo.transform.rotation = area.transform.rotation;

            objectiveInScene++;
        }
        StartCoroutine(SpawnObjectives());
    }
    private IEnumerator SpawnExtra()
    {
        yield return new WaitForSeconds(6);

        Vector3 randomSpawn = GetRandomSpawn(); //donde spawnea
        GameObject newExtra = Instantiate(prefabExtra, randomSpawn, area.transform.rotation);

        yield return new WaitForSeconds(0.8f);

        Destroy(newExtra);

        StartCoroutine(SpawnExtra());
    }

    private Vector3 GetRandomSpawn()
    {
        //area
        float ancho = 10f * area.transform.localScale.x;
        float alto = 10f * area.transform.localScale.z;

        //  posicion random del area
        float x = Random.Range(-ancho / 2f, ancho / 2f);
        float z = Random.Range(-alto / 2f, alto / 2f);
        Vector3 posicionLocal = new Vector3(x, 0, z);
        Vector3 posicionMundial = area.transform.TransformPoint(posicionLocal);

        posicionMundial += area.transform.up * 0.1f; //aparece frente al area

        return posicionMundial;
    }

    public void OnObjectiveClicked(GameObject clickedObjective)
    {
        clickedObjective.SetActive(false);
        objetivoQueue.Enqueue(clickedObjective);
        GameManager.instance.score += 1;
        objectiveInScene--;
    }

}
