using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] allPanel;
    private bool[] panelOpened = { false, false, false ,false};
    //public Text DispText;

    private void Start()
    {
        for (int i = allPanel.Length - 1; i >= 0; i--)
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

            if (index == 0 || index == 1)
            {
                MainScreenUi.isAnyOtherPanelOpened = true;
                MainScreenUi.panalOpened = allPanel[index];
            }
            if(index == 0)
            {
                allPanel[1].SetActive(false);
                panelOpened[1] = false;
            }
            else if (index == 1)
            {
                allPanel[0].SetActive(false);
                panelOpened[0] = false;
            }
            else
            {
                for (int i = 0; i < allPanel.Length; i++)
                {
                    if (index != i && i != 0)
                    {
                        allPanel[i].SetActive(false);
                        panelOpened[i] = false;
                    }
                }
            }

        }
        else
        {
            allPanel[index].SetActive(false);
            panelOpened[index] = false;
            MainScreenUi.isAnyOtherPanelOpened = false;
            MainScreenUi.panalOpened = allPanel[index];
        }
    }
    public void MakeAllPanelsFasle()
    {
        for(int i = 0;i < allPanel.Length;i++)
        {
            panelOpened[i] = false;
        }
    }

}
