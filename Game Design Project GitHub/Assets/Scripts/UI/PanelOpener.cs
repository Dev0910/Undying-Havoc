using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    private bool panelOpened;
    //public Text DispText;
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B) && panelOpened)
        {
            Panel.SetActive(false);
            panelOpened = false;
        }
        else if(Input.GetKeyUp(KeyCode.B) && !panelOpened)
        {
            Panel.SetActive(true);
            panelOpened = true;
        }
    }

    public void OpenPanel()
    {
        if(Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
    }

    
    /*public void SetText(string text)
    {
        DispText.text = text;
    }*/

}
