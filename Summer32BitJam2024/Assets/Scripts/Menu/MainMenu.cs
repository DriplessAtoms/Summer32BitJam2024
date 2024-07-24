using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        //SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }
}
