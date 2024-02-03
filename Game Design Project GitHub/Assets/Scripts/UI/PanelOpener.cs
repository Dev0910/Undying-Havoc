using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject[] allPanel;
    private bool[] panelOpened = { false, false, false };
    //public Text DispText;

    private void Start()
    {
        for(int i = allPanel.Length-1;i >= 0;i--)
        {
            allPanel[i].SetActive(false);
            panelOpened[i] = false;
        }
    }
    public void OpenPanal(int index)
    {
        if (!panelOpened[index])
        {
            if (MainScreenUi.GameIsPaused) return;
            allPanel[index].SetActive(true);
            panelOpened[index] = true;

            for (int i = 0; i < allPanel.Length; i++)
            {
                if (index != i && i != 0)
                {
                    allPanel[i].SetActive(false);
                    panelOpened[i] = false;
                }
            }
        }
        else
        {

            allPanel[index].SetActive(false);
            panelOpened[index] = false;
        }
    }
}
