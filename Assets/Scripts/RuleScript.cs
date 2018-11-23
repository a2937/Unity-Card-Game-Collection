using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RuleScript : MonoBehaviour
{
    public string MenuSceneName = "MainMenu"; 

    public List<GameRules> rules;

    public int indexOfDisplayedRules = 0; 

    public Text TitleText;

    public Text RulesText;

    public void Awake()
    {
        if(rules.Count > 0)
        {
            TitleText.text = rules.Get(0).Title;
            RulesText.text = rules.Get(0).Rules;
        }
    }

    public void GetNextRulesInList()
    {
        if(indexOfDisplayedRules == rules.Capacity - 1)
        {
            return; 
        }
        else
        {
            ++indexOfDisplayedRules;
            TitleText.text = rules.Get(indexOfDisplayedRules).Title;
            RulesText.text = rules.Get(indexOfDisplayedRules).Rules;
        }
    }

    public void GetPreviousRulesInList()
    {
        if (indexOfDisplayedRules == 0)
        {
            return;
        }
        else
        {
            --indexOfDisplayedRules;
            TitleText.text = rules.Get(indexOfDisplayedRules).Title;
            RulesText.text = rules.Get(indexOfDisplayedRules).Rules;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }


}
