using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON; //Esta libreria es necesaria para poder leer el formato JSON que nos manda la API
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class WeatherApiHandler : MonoBehaviour
{
    [SerializeField] WeatherData weatherData;
    [SerializeField] private string latitude;
    [SerializeField] private string longitude;
    [SerializeField] private string url;

    private string jsonRaw;

    [Header("Post-Process")]
    [SerializeField] private Light directionalLight;
    [SerializeField] private float lightColorTransitionSpeed;
    [SerializeField] private Volume globalVolume;
    private ChromaticAberration chrom;
    private ColorAdjustments colorAdjus;

    [Header("Weather Change")]
    [SerializeField] private float locationChangeTime = 10f;
    public List<Coordinates> coordinatesList;
    private Coordinates currentCoord;
    [SerializeField] private TextMeshProUGUI ciudad;

    private void OnEnable()
    {
        SelectCoordinates();
        UpdateUrl();
        StartCoroutine(WeatherUpdate());
        globalVolume.profile.TryGet<ChromaticAberration>(out chrom);
        globalVolume.profile.TryGet<ColorAdjustments>(out colorAdjus);
    }

    private void SelectCoordinates()
    {
        if (coordinatesList.Count == 0) return; //asegurarnos que haya algo

        //Seleccionar un lugar random
        int randomIndex = UnityEngine.Random.Range(0, coordinatesList.Count);
        currentCoord = coordinatesList[randomIndex];
        latitude = currentCoord.latitude;
        longitude = currentCoord.longitude;
        ciudad.text = currentCoord.name;
        Debug.Log("Lugar seleccionadas: " + currentCoord.name);
    }

    private void UpdateUrl()
    {
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&appid=7fe45acb4f5a69f83c45312aad97613a&units=metric";
    }

    IEnumerator WeatherUpdate()
    {
        UnityWebRequest request = new UnityWebRequest(url);    //Nos guarda la solicitud que queremos realizar a la web
        request.downloadHandler = new DownloadHandlerBuffer(); //Nos dice que queremos descargar el contenido de la web en un bucle

        yield return request.SendWebRequest();                 //Esta linea envia la solicitud a la web y espera a que se complete

        if (request.result != UnityWebRequest.Result.Success)   //si la solicitud n se pudo hacer
        {
            Debug.Log(request.error);
            StopCoroutine(WeatherUpdate()); //Detiene la corrutina si la solicitud falla
        }
        else
        {
            jsonRaw = request.downloadHandler.text;          //Esta linea guarda el contenido en una variable
            Debug.Log("Weather data received successfully!");
            Debug.Log(jsonRaw);                              //esta linea imprime el contenido de la web en la consola
            DecodeJson();
        }
    }

    private void DecodeJson()
    {
        //transforma el string a un JSON legible. Esta varibale JSONNode es una clase que nos permite leer el formato de json que nos manda la API
        JSONNode json = JSON.Parse(jsonRaw); //el parse es como un convert.ToInt32(texto), te ayuda a transformar una variable a otra como un texto a numero

        string timezone = json["timezone"];
        weatherData.continent = timezone.Split('/')[0];
        weatherData.city = timezone.Split('/')[1];
        weatherData.actualTemp = json["current"]["temp"]; //aqui se obtiene una variable que tiene otra variable
        weatherData.description = json["current"]["weather"][0]["description"];
        weatherData.windSpeed = json["current"]["wind_speed"];
        Debug.Log("Timezone: " + timezone);

        UpdateVolume();
        StartCoroutine(UpdateLocation());
        UpdateDirectionalLight();
    }

    IEnumerator UpdateLocation()
    {
        yield return new WaitForSeconds(locationChangeTime);
        SelectCoordinates();
        UpdateUrl();
        StartCoroutine(WeatherUpdate());
    }

    Color colorFilter;
    private void UpdateDirectionalLight()
    {
        //Revisa la temperatura para cambiar de color
        switch (weatherData.actualTemp)
        {
            case float when weatherData.actualTemp < 0:
                {
                    colorFilter = new Color(75f/255f, 187f/255f, 1); //celeste muy claro
                    break;
                }
            case float when weatherData.actualTemp >= 0 && weatherData.actualTemp <= 10:
                {
                    colorFilter = new Color(179f / 255f, 250f/255f, 1); //celeste 
                    break;
                }
            case float when weatherData.actualTemp > 10 && weatherData.actualTemp <= 25:
                {
                    colorFilter = new Color(1f, 208f / 255f, 178f/255f); //Naranja muy muy claro
                    break;
                }
            case float when weatherData.actualTemp > 25 && weatherData.actualTemp < 40:
                {
                    colorFilter = new Color(1f, 165f/255f, 115f/255f ); //Naranja fuerte
                    break;
                }
            case float when weatherData.actualTemp >= 40:
                {
                    colorFilter = new Color(1, 129f/255f, 94f/255f); //Rojo
                    break;
                }
            default:
                {
                    colorFilter = Color.white/255f;
                    break;
                }
        }
        StartCoroutine(ChangeColorFilter());
    }

    Color targetColor;
    IEnumerator ChangeColorFilter()
    {
        float duration = 2f;
        float timePassed = 0f;

        targetColor = colorFilter;
        Color startColor = colorAdjus.colorFilter.value;
        Debug.Log("Cambiando el color");

        while (timePassed < duration)
        {
            float t = timePassed / duration;

            colorAdjus.colorFilter.value = Color.Lerp(startColor, targetColor, t);

            timePassed += Time.deltaTime;
            yield return null;
        }

        colorAdjus.colorFilter.value = targetColor;
        Debug.Log("Se cambio el color a:  " + targetColor);
    }

    float targetIntensity;
    private void UpdateVolume()
    {
        switch (weatherData.actualTemp)
        {
            case float temp when weatherData.actualTemp < 0 && weatherData.actualTemp <= 20:
                {
                    targetIntensity = 0;
                    break;
                }

            case float temp when weatherData.actualTemp > 20 && weatherData.actualTemp <= 30:
                {
                    targetIntensity = 0.5f;
                    break;
                }

            case float temp when weatherData.actualTemp > 30:
                {
                    targetIntensity = 1;
                    break;
                }
            default:
                {
                    targetIntensity = 0;
                    break;
                }
        }
        StartCoroutine(ChromChange());
    }

    IEnumerator ChromChange()
    {
        float duration = 2f;
        float timePassed = 0f;

        float startIntensity = chrom.intensity.value;
        Debug.Log("Cambiando el chrom");

        while (timePassed < duration)
        {
            float t = timePassed / duration;

            chrom.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, t);

            timePassed += Time.deltaTime;
            yield return null;
        }

        chrom.intensity.value = targetIntensity;
        Debug.Log("Se cambio el chrom a:  " + targetIntensity);
    }
}     

[Serializable]
public struct WeatherData
{
    public string continent;
    public string city;
    public float actualTemp;
    public string description;
    public float windSpeed;
}

[Serializable]
public struct Coordinates
{
    public string name;
    public string latitude;
    public string longitude;
}

