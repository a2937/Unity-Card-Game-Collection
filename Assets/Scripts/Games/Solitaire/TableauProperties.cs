using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauProperties : ICardStack
{
    private SolitaireCardProperties[] Deck = new SolitaireCardProperties[7];

    private int top = -1;
    private int count = 0;
    private bool gameStarted = false;




    public void SetGameStarted(bool began)
    {
        gameStarted = began; 
    }

    public bool Add(SolitaireCardProperties newItem)
    {
        if(IsFull())
        {
            return false; 
        }
        else if (!gameStarted && !IsFull())
        {
            Deck[count] = newItem;
            count++;
            top++;
            return true;
        }
        else if (gameStarted && !IsFull())
        {
            if (newItem.CompareTo(Deck[top]) > 0 && newItem.IsBlack() != Deck[top].IsBlack() && !IsFull())
            {
                Deck[count] = newItem;
                count++;
                top++;
                return true; 
            }
            else
            {
                return false; 
            }

        }

        return false;
    }

    public int GetCount()
    {
        return count; 
    }

    public IEnumerator<SolitaireCardProperties> GetEnumerator()
    {
        return (IEnumerator<SolitaireCardProperties>)Deck.GetEnumerator(); 
    }

    public bool IsEmpty()
    {
        return count == 0 || top == -1;
    }

    public bool IsFull()
    {
        return count == 7;
    }

    public SolitaireCardProperties Peek()
    {
        if (top == -1)
        {
            return null;
        }
        if (Deck[top] == null)
        {
            return null;
        }
        else
        {
            return Deck[top]; 
        }
    }

    public SolitaireCardProperties Remove()
    {
        if(count == 0)
        {
            return null;
        }
        SolitaireCardProperties card = Deck[top];
        Deck[top] = null;
        top--;
        count--;
        return card; 
    }

    public void RemoveAll()
    {
        for(int i = 0; i < count; i++)
        {
            Deck[i] = null; 
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); 
    }
}
