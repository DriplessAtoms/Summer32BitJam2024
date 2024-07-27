using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mainMixer;

    void Start()
    {
        Screen.SetResolution(640, 480, true);
    }

    public void PlayGame() {
        //SceneManager.LoadSceneAsync(1);
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void QuitGame() {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void SetVolumeMusic(float sliderValue)
    {
        mainMixer.SetFloat("Music", sliderValue);
    }

    public void SetVolumeSFX(float sliderValue)
    {
        mainMixer.SetFloat("SFX", sliderValue);
    }
}
