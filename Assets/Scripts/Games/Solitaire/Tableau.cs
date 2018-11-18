using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tableau : MonoBehaviour, ICardStackUIComponent
{
    private TableauProperties tableau;

    [SerializeField]
    private List<PlayingCard> cards; // These should be the children

    // Use this for initialization
    void Start()
    {
        if(cards == null)
        {
            cards = new List<PlayingCard>(); 
        }
    }

    public ICardStack GetCardStack()
    {
        return tableau;
    }

    public void SetCardStack(ICardStack stack)
    {
        if (stack is Tableau)
        {
            this.tableau = stack as TableauProperties;
        }
    }

    public void UpdateSprite()
    {
        foreach(PlayingCard playingCard in cards)
        {
            playingCard.UpdateSprite(); 
        }
    }
}
