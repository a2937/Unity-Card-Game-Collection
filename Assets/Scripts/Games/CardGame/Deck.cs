﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The type Deck for a card game.
 *
 * @author Aaron Cottrill
 */
 [Serializable]
public class Deck 
{
    [SerializeField]
    private List<CardProperties> deck = new List<CardProperties>();

    [SerializeField]
    private int top = 52;

    public Deck()
    {
        Init(false); 
    }

    public Deck(bool isBlackJackDeck)
    {
        Init(isBlackJackDeck);
    }

    private void Init(bool isBlackJackDeck)
    {
        if(isBlackJackDeck)
        {
            InitializeBlackJackDeck();
        }
        else
        {
            InitializePokerDeck(); 
        }
    }

    private void InitializeBlackJackDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int value = 1; value < 14; value++)
            {
                if (value <= 10 && value != 1)
                {
                    deck.Add(new CardProperties(value, suit, value.ToString()));
                }

                else if (value == 1)
                {
                    deck.Add(new CardProperties(value, suit, "Ace"));
                }

                else if (value == 11)
                {
                    deck.Add(new CardProperties(10, suit, "Jack"));
                }

                else if (value == 12)
                {
                    deck.Add(new CardProperties(10, suit, "Queen"));
                }

                else if (value == 13)
                {
                    deck.Add(new CardProperties(10, suit, "King"));
                }
            }
        }
        Shuffle();
    }

    private void InitializePokerDeck()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            for (int value = 1; value < 14; value++)
            {
                if (value <= 10 && value != 1)
                {
                    deck.Add(new CardProperties(value, suit, value.ToString()));
                }

                else if(value == 1)
                {
                    deck.Add(new CardProperties(value, suit, "Ace"));
                }

                else if (value == 11)
                {
                    deck.Add(new CardProperties(value, suit, "Jack"));
                }

                else if (value == 12)
                {
                    deck.Add(new CardProperties(value, suit, "Queen"));
                }

                else if (value == 13)
                {
                    deck.Add(new CardProperties(value, suit, "King"));
                }
            }
        }
        Shuffle();
    }

    /**
     * Shuffle.
     */
    private void Shuffle()
    {
        deck.Shuffle(); 
    }

    /**
     * Deal card.
     *
     * @return the card
     */
    public CardProperties Deal()
    {
        top--;
        CardProperties card = deck[top];
        deck.RemoveAt(top);
        return card; 
    }


}
