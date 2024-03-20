using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PageManager : MonoBehaviour
{
    public GameObject[] pages;
    private int currentPageIndex;
    public string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        currentPageIndex = 0;
        pages[currentPageIndex].SetActive(true);
        for(int i  = 1; i < pages.Length; i++)
        {
            pages[(int)i].SetActive(false);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            NextPage();
        }
    }
    public void NextPage()
    {
        currentPageIndex++;
        if(currentPageIndex < pages.Length)
        {
            pages[currentPageIndex-1].SetActive(false);
            pages[currentPageIndex].SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(nextSceneName);
        }
        
    }

    public void PreviousPage()
    {
        if(currentPageIndex <= 0) { return; }
        pages[currentPageIndex].SetActive(false);
        currentPageIndex--;
        pages[currentPageIndex].SetActive(true);
    }

}
