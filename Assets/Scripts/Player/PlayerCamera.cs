using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player camera movement script
public class PlayerCamera : MonoBehaviour
{
    public bool permanentAttach;
    public bool tempAttach;

    // camera settings
    [Header("Settings")]
    [SerializeField] private int border = 50;

    [Header("Player")]
    [SerializeField] private GameObject _player;


    private float maxSpeed = 40f;
    private Vector3 _attachOffset;

    //===============================

    private void Awake() 
    {
        if(_player == null) 
            Debug.LogError("PlayerCamera: Player was not found!");

        Cursor.lockState = CursorLockMode.Confined;
        _attachOffset = transform.position - _player.transform.position;
        _attachOffset.x = 0f;
    }

    private void LateUpdate() 
    {
        if(permanentAttach || tempAttach)
        {
            transform.position = _player.transform.position + _attachOffset;
        }
    }

    public void MoveCamera(Vector2 mousePosition)
    {
        if(permanentAttach || tempAttach) return;

        Vector3 newPos = transform.position;
        if(mousePosition.x <= border)
        {
            newPos -= transform.right * GameManager.CameraSpeed * maxSpeed *  Time.deltaTime;
        }
        else if (mousePosition.x >= Screen.width - border)
        {
            newPos += transform.right * GameManager.CameraSpeed * maxSpeed  *  Time.deltaTime;
        }

        if(mousePosition.y <= border)
        {
            newPos -= transform.forward * GameManager.CameraSpeed * maxSpeed  *  Time.deltaTime;
        }
        else if(mousePosition.y >= Screen.height - border)
        {
            newPos += transform.forward * GameManager.CameraSpeed * maxSpeed  * Time.deltaTime;
        }
        transform.position = newPos;
    }
}
