using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStack<T> : IEnumerable<T>
{

    /**
     * Adds a new item to the stack.
     * 
     * Returns true or false if it is successful.
     */
    bool Add(T newItem);

    void RemoveAll(); 

    /**
     * Removed the item at the top 
     * of the stack. If there is none,
     * then null is returned. 
     */
    T Remove();

    /**
     * Returns the item at the top of the 
     * stack without modifying the stack. 
     */
    T Peek();

    /**
     * Returns true if the stack is full
     */
    bool IsFull();

    /**
     * Returns true if the stack is empty 
     */
    bool IsEmpty();

    /*
     * Returns the amount of items in 
     * the stack. 
     */
    int GetCount(); 
}
