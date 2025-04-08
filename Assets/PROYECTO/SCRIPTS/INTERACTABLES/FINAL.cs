using UnityEngine;
using UnityEngine.SceneManagement;

public class FINAL : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Final");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
