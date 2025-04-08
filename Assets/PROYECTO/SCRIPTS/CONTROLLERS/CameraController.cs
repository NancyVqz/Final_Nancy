using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float mouseSensitivity = 2; // Sensibilidad
    private float smoothness = 6; // Suavidad

    private float maxAngleY = 80;
    private float minAngleY = -80;

    private Vector2 camVelocity;
    private Vector2 smoothVelocity; 

    private Transform player;

    private void Start()
    {
        if(transform.parent != null)
        {
            if (transform.parent.TryGetComponent<Transform>(out player))
            {
                Debug.Log("Se encontro el player");
            }
            else
            {
                Debug.LogWarning("No se encontro al player");
            }
        }
        else
        {
            Debug.LogWarning("La camara no esta emparentada");
        }        

    }

    void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {       
        Vector2 rawMouseVelocity = Vector2.Scale(MousePos(), Vector2.one * mouseSensitivity); 
        smoothVelocity = Vector2.Lerp(smoothVelocity, rawMouseVelocity,1 / smoothness); 
        camVelocity += smoothVelocity; 
        camVelocity.y = Mathf.Clamp(camVelocity.y,minAngleY,maxAngleY); 

        transform.localRotation = Quaternion.AngleAxis(-camVelocity.y, Vector3.right);
        player.localRotation = Quaternion.AngleAxis(camVelocity.x, Vector3.up);
    }

    private Vector2 MousePos()
    {
        return new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); 
    }

}
