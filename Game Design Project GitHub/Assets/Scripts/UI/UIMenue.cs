using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIMenue : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject panelToDesable;
    public GameObject pauseMenuUI;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        panelToDesable.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        panelToDesable.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void onBtnClick(int scene)
    {
        SceneManager.LoadScene(scene);
    }

}
