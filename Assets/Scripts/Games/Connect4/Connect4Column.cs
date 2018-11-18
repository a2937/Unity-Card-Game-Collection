using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4Column : IStack<Connect4Token>
{
    private List<Connect4Token> tokens = new List<Connect4Token>();

    [SerializeField]
    private int columnSize; 



    public Connect4Column(int size)
    {
        SetColumnSize(size); 
    }

    /**
     * Adds a token to the column, if the amount of 
     * tokens is not at maximum capacity 
     * 
     * Returns true or false depending on if it 
     * is successful. 
     */
    public bool Add(Connect4Token newItem)
    {
        if(tokens.Count < columnSize)
        {
            tokens.Set(tokens.Capacity, newItem); 
            return true;
        }
        else
        {
            Debug.Log("Connect4Column.AddToken() capacity reached");
            return false;
          
        }
    }

    public Connect4Token Peek()
    {
        return tokens.Get(tokens.Count - 1);
    }

    public void SetColumnSize(int size)
    {
        if(size > 0)
        {
            columnSize = size; 
        }
    }

    public int GetColumnSize()
    {
        return columnSize; 
    }

    /**
     * In a game of Connect 4, 
     * tokens cannot be removed mid-game.
     * However for completion purposes, the method
     * will be included anyway. 
     */
    public Connect4Token Remove()
    {
        Connect4Token token = tokens[tokens.Count - 1]; 
        tokens.RemoveAt(tokens.Count - 1);
        return token; 
    }

    public bool IsFull()
    {
        return tokens.Count == columnSize;
    }

    public bool IsEmpty()
    {
        return tokens.Count == 0; 
    }

    public int GetCount()
    {
        return tokens.Count; 
    }

    public void RemoveAll()
    {
        for(int i = 0; i < tokens.Count; i++)
        {
            tokens[i] = null; 
        }
    }

    public IEnumerator<Connect4Token> GetEnumerator()
    {
        return tokens.GetEnumerator(); 
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
