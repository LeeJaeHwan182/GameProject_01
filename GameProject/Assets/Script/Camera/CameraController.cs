using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject PlayerCamera;

    public static bool On_Off = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(On_Off)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                On_Off = false;
                MainCamera.SetActive(false);
                PlayerCamera.SetActive(true);
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                BuildManager.instance.ResetCamera();
                On_Off = true;
                MainCamera.SetActive(true);
                PlayerCamera.SetActive(false);
            }
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        BuildManager.instance.ResetCamera();
        On_Off = true;
        MainCamera.SetActive(true);
        PlayerCamera.SetActive(false);
    }

    public void ResetGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        On_Off = false;
        MainCamera.SetActive(false);
        PlayerCamera.SetActive(true);
    }
}
