using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainScreenUi : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject[] allPanel;
    public int pausePanalIndex;
    public static bool isAnyOtherPanelOpened;
    public static GameObject panalOpened;

    private void Start()
    {
        isAnyOtherPanelOpened = false;
        GameIsPaused = false;
        allPanel[pausePanalIndex].SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isAnyOtherPanelOpened)
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
        else if (Input.GetKeyDown(KeyCode.Escape) && isAnyOtherPanelOpened)
        {
            MainScreenUi.panalOpened.SetActive(false);
            MainScreenUi.isAnyOtherPanelOpened = false;
            GameObject.Find("PanelManager").GetComponent<PanelManager>().MakeAllPanelsFasle();
        }
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        allPanel[pausePanalIndex].SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        allPanel[pausePanalIndex].SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void OpenPanal(int index)
    {
        allPanel[index].SetActive(true);
        isAnyOtherPanelOpened = true;
        panalOpened = allPanel[index];

    }
    public void ClosePanal()
    {
        panalOpened.SetActive(false);
        isAnyOtherPanelOpened = false;
    }

}
