using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCellProperties : ICardStack
{
    private CardProperties card;


    /**
     * Instantiates a new Free cell.
     */
    public FreeCellProperties()
    {

    }

    public bool Add(CardProperties newItem)
    {
        if(card == null)
        {
            card = newItem;
            return true; 
        }
        else
        {
            return false;
        }
    }

    public int GetCount()
    {
        if (card == null)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public IEnumerator<CardProperties> GetEnumerator()
    {
       yield return card; 
    }

    public bool IsEmpty()
    {
        return card == null;
    }

    public bool IsFull()
    {
        return card != null;
    }

    public CardProperties Peek()
    {
        return card;
    }

    public CardProperties Remove()
    {
        CardProperties temp = card;
        card = null;
        return temp;
    }

    public void RemoveAll()
    {
        card = null; 
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); 
    }
}
