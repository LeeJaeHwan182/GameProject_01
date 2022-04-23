using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string levelToLoad = "Main";

    public GameObject Road;
    public void Play()
    {
        StartCoroutine("Roading");
    }
    public void Quit()
    {
        Debug.Log("나가지다");
        Application.Quit();
    }

    IEnumerator Roading()
    {
        Road.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelToLoad);
    }
}
