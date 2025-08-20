using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text scoreText;
    public Image userAvatarImage;

    public void SetPlayerInfo(string username, int score, Sprite avatar)
    {
        playerNameText.text = username;
        scoreText.text = score.ToString();
        userAvatarImage.sprite = avatar;
    }

}
