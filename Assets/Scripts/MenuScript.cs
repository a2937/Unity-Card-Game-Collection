using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string FiveCardStudSceneName = "FiveCardStud";

    public string BlackJackSceneName = "BlackJack";

    public string RulesSceneName = "Rules";

    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void PlayFiveCardStud()
    {
        SceneManager.LoadScene(FiveCardStudSceneName);
    }

    public void PlayBlackJack()
    {
        SceneManager.LoadScene(BlackJackSceneName);
    }

    public void OpenRules()
    {
        SceneManager.LoadScene(RulesSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
