using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject[] allPanel;
    public int startPanalIndex;
    private bool[] panelOpened = { false, false, false };
    private int currentPanalIndex;
    //public Text DispText;

    private void Start()
    {
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OpenPanal(currentPanalIndex);
        }
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

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadSeneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
    
}
