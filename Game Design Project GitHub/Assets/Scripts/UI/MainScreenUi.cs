using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenUi : MonoBehaviour
{

    public static bool GameIsPaused = false;
    //public GameObject pauseMenuUI;
    public GameObject[] allPanel;
    public int startPanalIndex;
    private bool[] panelOpened = { false, false, false };
    private int currentPanalIndex;

    private void Start()
    {
        GameIsPaused = false;
        allPanel[startPanalIndex].SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && currentPanalIndex == startPanalIndex)
            {
                Resume();

            }
            else
            {
                Pause();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape) && GameIsPaused)
        {
            OpenPanal(currentPanalIndex);
        }
    }

    public void Resume()
    {
        allPanel[startPanalIndex].SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        allPanel[startPanalIndex].SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        for (int i = allPanel.Length - 1; i >= 0; i--)
        {
            if (i == startPanalIndex)
            {
                allPanel[i].SetActive(true);
                panelOpened[i] = true;
                currentPanalIndex = i;
            }
            else
            {
                allPanel[i].SetActive(false);
                panelOpened[i] = false;
            }

        }
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenPanal(int index)
    {
        if (index == startPanalIndex)
        {
            return;
        }
        if (!panelOpened[index])
        {
            allPanel[index].SetActive(true);
            panelOpened[index] = true;
            currentPanalIndex = index;
        }
        else
        {

            allPanel[index].SetActive(false);
            panelOpened[index] = false;
            currentPanalIndex = startPanalIndex;
        }

    }

}
