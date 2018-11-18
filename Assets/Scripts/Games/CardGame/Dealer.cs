using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dealer : Player
{
    [SerializeField]
    private Deck deck;

    /**
     * Instantiates a new Dealer.
     *
     * @param name  the name
     * @param stash the stash
     */
    public Dealer(String name, int stash) : base(name,stash)
    {
        deck = new Deck();
    }

    /**
    * Instantiates a new Dealer.
    *
    * @param name  the name
    * @param stash the stash
    * @param isBlackJack whether we are playing blackjack or not
    */
    public Dealer(String name, int stash, bool isBlackJack) : base(name, stash)
    {
        deck = new Deck(isBlackJack);
    }

    public Dealer()
    {
        SetName("Dealer");
        SetStash(2500);
        deck = new Deck(); 
    }

    /**
     * Deal first card to player card.
     *
     * @param player the player
     * @return the card
     */
    public CardProperties DealFirstCardToPlayer(Player player)
    {
        CardProperties tempCard = deck.Deal();
        tempCard.Hide();
        player.AddCard(tempCard);
        return tempCard;
    }

    public CardProperties DealToPlayer(Player player)
    {
        CardProperties tempCard = deck.Deal();
        tempCard.Show();
        player.AddCard(tempCard);
        return tempCard;
    }

    /**
     * @param stash2 the stash to set
     * If they don`t have any money, 
     * the house will let them have fifty dollars
     * to get them started. 
     * 
     */
    public void setStash(float stash2)
    {
        if (stash2 > 1)
        {
            this.stash = stash2;
        }

        else
        {
            this.stash = 5000000;
        }
    }
}
