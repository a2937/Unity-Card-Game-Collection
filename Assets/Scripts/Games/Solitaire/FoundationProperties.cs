using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The type Foundation for a game of FreeCell solitaire.
 * These are the columns in the top left hand corner.
 * @author aaron
 */
public class FoundationProperties : ICardStack
{
    private CardProperties[] Deck = new CardProperties[14];

    private int top = -1;
    private int count = 0;
    private Suit suit;

    /**
     * Creates a brand new Foundation object
     * and setting the suit to be collected.
     *
     * @param newSuit : The suit object this foundation uses
     */
    public FoundationProperties(Suit newSuit)
    {
        suit = newSuit;
    }

    /**
     * Creates a brand new Foundation object
     * and setting the suit to be collected.
     *
     * @param suitOrdinal : The ordinal that represents the Suit instance we want
     */
    public FoundationProperties(int suitOrdinal)
    {
        SetSuit(suitOrdinal);
    }

    private void SetSuit(int ordinal)
    {
        if(Enum.IsDefined(typeof(Suit),ordinal))
        {
            suit = (Suit)ordinal; 
        }
    }

    public Suit GetSuit()
    {
        return suit; 
    }

    public bool Add(CardProperties newItem)
    {
        if (IsFull())
        {
            return false;
        }
        if (count == 0 && newItem.GetSuit() == this.suit)
        {
            Deck[count] = newItem;
            count++;
            top++;
            return true; 
        }

        else if (newItem.GetSuit() == this.suit && newItem.GetValue() == GetNextCardValue())
        {
            Deck[count] = newItem;
            count++;
            top++;
            return true; 
        }

        return false;
    }

    /**
      * Gets next expected.
      *
      * @return the next expected
      */
    private int GetNextCardValue()
    {
        return count + 1;
    }

    public int GetCount()
    {
        return count; 
    }

    public IEnumerator<CardProperties> GetEnumerator()
    {
        return (IEnumerator<CardProperties>)Deck.GetEnumerator(); 
    }

    public bool IsEmpty()
    {
        return count == 0;
    }

    public bool IsFull()
    {
        return count == 13;
    }

    public CardProperties Peek()
    {
        if (top != -1)
        {
            return Deck[0];
        }
        else
        {
            return Deck[count];
        }
    }

    /**
     * Determines if this stack of cards
     * has every card from in the right position
     * in order of card rank.
     *
     * @return a boolean representing if the foundation is complete
     */
    public bool IsComplete()
    {
        if (IsFull())
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public void RemoveAll()
    {
        for(int i = 0; i < 14; i++)
        {
            Deck[i] = null;

        }
        count = 0;
        top = -1; 
    }

    public CardProperties Remove()
    {
        if(IsEmpty())
        {
            return null;
        }
        else
        {
            CardProperties temp = Deck[top];
            Deck[top] = null;
            top--;
            count--;
            return temp;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override String ToString()
    {
        String contents = "";
        if (IsEmpty())
        {
            contents += "  ";
        }
        else
        {
            contents += Peek();
        }
        /*
        for(int i = 0; i < 13 ; i++)
        {
            contents += deck[i] + " ,";
        }
        contents += deck[13];
        */
        return contents;
    }
}
