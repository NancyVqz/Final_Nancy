using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayFabHandler : MonoBehaviour
{
    private string titleID = "1B36C0";
    //private string developerKey = "F6TGI9NYOKQNCUMNGFGN7K5PG6GSCNN9U6619DHADBOY9BA8P8";
    public int score;
    private string currentPlayFabId;

    [Header("Register UI Elements")]
    [SerializeField] private TMP_InputField register_UsernameInputField;
    [SerializeField] private TMP_InputField register_EmailInputField;
    [SerializeField] private TMP_InputField register_PasswordInputField;
    [SerializeField] private TMP_InputField register_ConfirmInputField;
    [SerializeField] private UnityEvent OnRegisSuccess;

    [Header("Login UI Elements")]
    [SerializeField] private TMP_InputField login_UsernameInputField;
    [SerializeField] private TMP_InputField login_PasswordInputField;
    [SerializeField] private UnityEvent OnLogSuccess;

    [SerializeField] private string userDisplayName;
    [SerializeField] private Image userAvatarImage;
    [SerializeField] private string userAvatarURL;
    [SerializeField] private TMP_Text userDisplayNameText;

    [Header("Leaderboard UI")]
    [SerializeField] private GameObject leaderboardContainer;
    [SerializeField] private GameObject leaderboardPrefab;
    private Sprite currentUserAvatar;

    void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = titleID;
        }

        //if (string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        //{
        //    PlayFabSettings.DeveloperSecretKey = developerKey;
        //}  
    }

    //Username: Nombre de usuario, 
    //DisplayName: Nombre que se mostrara en el juego, no es necesario que sea unico
    //Email: correo electronico del ususario
    //Contraeña: Minimo 8 caracteres

    public void CreatePlayfabAccount()
    {
        if (register_PasswordInputField.text != register_ConfirmInputField.text)
        {
            Debug.LogError("Password do nto match!");
            return; //si no coinciden las contraseñas, no continua el registro
        }

        RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest
        {
            DisplayName = register_UsernameInputField.text, //nombre que se muestra en el juego
            Username = register_UsernameInputField.text,  //nombre que sirve para iniciar sesion
            Email = register_EmailInputField.text,
            Password = register_PasswordInputField.text,
            RequireBothUsernameAndEmail = true
        };

        //aqui se ejecuta el request, ya se manda
        //                             solicitud / lo que pasa si sale bien / si sale mal
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, ErrorMessage);

    }
    //este metodo se ejecuta si el registro es exitoso
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("User register successfully");
        OnRegisSuccess.Invoke();
    }

    public void LoginUser()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest
        {
            Username = login_UsernameInputField.text,
            Password = login_PasswordInputField.text,
        };

        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, ErrorMessage);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("User loged in successfully");
        currentPlayFabId = result.PlayFabId;
        OnLogSuccess.Invoke();
    }

    [ContextMenu("Get Player Profile")]
    public void GetPlayerProfile()
    {
        GetPlayerProfileRequest request = new GetPlayerProfileRequest //solicitud de datos que quiera conseguir
        {
            ProfileConstraints = new PlayerProfileViewConstraints
            {
                ShowDisplayName = true,
                ShowAvatarUrl = true,
            },
        };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetPlayerProfileSuccess, ErrorMessage);
    }

    private void OnGetPlayerProfileSuccess(GetPlayerProfileResult result)
    {
        userDisplayName = result.PlayerProfile.DisplayName;
        userAvatarURL = result.PlayerProfile.AvatarUrl;

        userDisplayNameText.text = userDisplayName;
        StartCoroutine(RetrievePlayerAvatar()); //inicia la ecorutina para descargar la imagen del avatar
    }

    private Sprite userAvatarSprite;
    IEnumerator RetrievePlayerAvatar()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(userAvatarURL); //solicitud a la web para conseguir una imagen

        yield return request.SendWebRequest(); //envia la solicitud y espera que se complete

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            StopAllCoroutines();
        }
        else
        {
            DownloadHandlerTexture downloadHandler = request.downloadHandler as DownloadHandlerTexture; //guarda la imagen que consegui en una variable
            Sprite playerImage = Sprite.Create(downloadHandler.texture, new Rect(0.0f, 0.0f, downloadHandler.texture.width, downloadHandler.texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            userAvatarImage.sprite = playerImage; //asigno la imagen que consegui al componente UI

            currentUserAvatar = userAvatarImage.sprite;

        }
    }

    [ContextMenu("Update Score")]
    public void UpdateScore()
    {
        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "High Score", //Nombre de la tabla del playfab que pusiste al crear la tabla
                    Value= score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnUpdateStatisticsSuccess, ErrorMessage);
    }

    private void OnUpdateStatisticsSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Player statistics updated successfully");
    }

    public void GetLeaderboard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest
        {
            StatisticName = "High Score", //nombre de la tabla que le puse
            StartPosition = 0,
            MaxResultsCount = 100
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, ErrorMessage);
    }

    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("Leaderboard retrieve successfully");

        foreach (Transform child in leaderboardContainer.transform) //limpiar el leaderboard
        {
            Destroy(child.gameObject);
        }

        foreach (PlayerLeaderboardEntry user in result.Leaderboard) //el for each nos sirve para revisar todos los elementos de la lista
        {
            Debug.Log($"Player: {user.DisplayName}, Score: {user.StatValue}");

            GameObject newEntry = Instantiate(leaderboardPrefab, leaderboardContainer.transform);
            newEntry.transform.localScale = Vector3.one;

            UIItem uiItem = newEntry.GetComponent<UIItem>();
            if (uiItem != null)
            {

                if (user.PlayFabId == currentPlayFabId) //checa si es la misma id loggeada para mostrar su avatar
                {
                    Debug.LogWarning("Obteniendo imagen de player");
                    uiItem.SetPlayerInfo(user.DisplayName, user.StatValue, currentUserAvatar);
                }
                else //sino es el jugador pone vacio
                {
                    uiItem.SetPlayerInfo(user.DisplayName, user.StatValue, null);
                }
            }
        }
    }

    //Este metodo nos va a servir para todos los errores que nos pueden ocurrir al hacer solicitudes a Playfab
    private void ErrorMessage(PlayFabError error)
    {
        Debug.LogError($" {error.Error} \n {error.ErrorMessage} \n {error.ErrorDetails}");
    }

}
