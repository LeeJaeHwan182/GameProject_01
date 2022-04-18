using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;
    public CameraController cameracontroller;
    public GameObject TowerUI;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if(ui.activeSelf)
        {
            TowerUI.SetActive(false);
            Time.timeScale = 0f;
            cameracontroller.GameOver();
        }
        else
        {
            TowerUI.SetActive(true);
            Time.timeScale = 1f;
            cameracontroller.ResetGame();
        }
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TextBox.GAMESTART = false;
        cameracontroller.ResetGame();
    }

    public void Menu()
    {
        Debug.Log("Go to menu");
    }
}
