using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    public CameraController cameracontroller;

    private void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TextBox.GAMESTART = false;
        cameracontroller.ResetGame();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        TextBox.GAMESTART = false;
        Time.timeScale = 1f;
        CameraController.On_Off = false;
    }
}
