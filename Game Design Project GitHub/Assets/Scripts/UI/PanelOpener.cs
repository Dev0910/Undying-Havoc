using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelOpener : MonoBehaviour
{
    public GameObject Panel;
    //public Text DispText;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Panel.SetActive(false);
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
