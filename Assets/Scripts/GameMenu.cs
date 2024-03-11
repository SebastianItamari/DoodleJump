using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool paused = false;

    void Start()
    {
        BackgroundSound.instance.ReproduceGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                Pause();
            }
            else if (paused == true)
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        AudioController.instance.SoundButton();
        paused = true;
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        AudioController.instance.SoundButton();
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        paused = false;
    }

    public void Restart()
    {
        AudioController.instance.SoundButton();
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }

    public void Quit()
    {
        BackgroundSound.instance.Stop();
        AudioController.instance.SoundButton();
        Time.timeScale = 1f;
        paused = false;
        SceneManager.LoadScene(0);
    }
}
