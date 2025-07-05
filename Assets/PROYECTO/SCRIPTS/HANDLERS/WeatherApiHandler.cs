using System.ComponentModel;
using System;
using UnityEngine;
using SimpleJSON; //Esta libreria es necesaria para poder leer el formato JSON que nos manda la API
using System.Collections;
using UnityEngine.Networking;
using Unity.VisualScripting;

public class WeatherApiHandler : MonoBehaviour
{
    [SerializeField] WeatherData weatherData;
    [SerializeField] private string latitude = "40.7128";
    [SerializeField] private string longitude = "-74.0060";
    [SerializeField, ReadOnly(default)] private string url;

    private string jsonRaw;

    private void OnValidate()
    {
        url = $"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&appid=7fe45acb4f5a69f83c45312aad97613a&units=metric";
    }

    private void Start()
    {
        StartCoroutine(WeatherUpdate());
    }


    IEnumerator WeatherUpdate()
    {
        UnityWebRequest request = new UnityWebRequest(url);    //Nos guarda la solicitud que queremos realizar a la web
        request.downloadHandler = new DownloadHandlerBuffer(); //Nos dice que queremos descargar el contenido de la web en un bucle

        yield return request.SendWebRequest();                 //Esta linea envia la solicitud a la web y espera a que se complete

        if(request.result != UnityWebRequest.Result.Success)   //si la solicitud n se pudo hacer
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Weather data received successfully!");
            jsonRaw = request.downloadHandler.text;          //Esta linea guarda el contenido en una variable
            Debug.Log(jsonRaw);                              //esta linea imprime el contenido de la web en la consola
            DecodeJson();
        }
    }
    public string[] timezone;
    private void DecodeJson()
    {
        //transforma el string a un JSON legible. Esta varibale JSONNode es una clase que nos permite leer el formato de json que nos manda la API
        JSONNode json = JSON.Parse(jsonRaw); //el parse es como un convert.ToInt32(texto), te ayuda a transformar una variable a otra como un texto a numero

        string timezone = json["timezone"]; //America/Monterrey
        string[] timezoneSplit = timezone.Split('/'); //Dividimos la cadena en partes usando / como separador
        
        weatherData.continent = timezoneSplit[0];
        weatherData.city = timezoneSplit[1];

        Debug.Log("Timezone: " + timezoneSplit);

        //float temp = json["current"]["temp"]; //aqui se obtiene una variable que tiene otra variable
        //string currentWeather = json["current"]["weather"][""]["main"]; //Aqui se ponen corchetes vacios para cuando hay un corchete vacio antes de la llave
        //string hourlyTemp = json["hourly"][""]["temp"];

    }
}

[Serializable]
public struct WeatherData
{
    public string continent;
    public string city;
    public string actualTemp;
    public string description;
    public string windSpeed; 
}