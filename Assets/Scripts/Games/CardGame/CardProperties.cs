using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class CardProperties : IComparable<CardProperties>
{
    [SerializeField]
    private int value;

    
    [SerializeField]
    private bool visible = true;

    [SerializeField]
    private String cardName;


  


    /**
     * @return the value
     */
    public int GetValue()
    {
        return value;
    }

    /**
     * @param newValue the value to set
     */
    public void SetValue(int newValue)
    {

        this.value = newValue;

    }

    /**
     * @return the name
     */
    public String GetName()
    {
        return cardName;
    }

    public void Show()
    {
        this.visible = true;
    }

    public void Hide()
    {
        this.visible = false;
    }

    public bool IsVisible()
    {
        return this.visible;
    }

    public virtual String GetSpriteName()
    {
        if(IsVisible())
        {
            return GetName(); 
        }
        else
        {
            return "back"; 
        }
    }

    /**
      * @param newName the name to set
      */
    public void SetName(String newName)
    {

        if (!newName.Equals(""))
        {
            this.cardName = newName;
        }

        else
        {
            Debug.Log("PlayingCard.SetName error :no card name given.");
        }

    }


    public override String ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (visible)
        {
            builder.Append(GetName()); 
        }

        else
        {
            builder.Append("Some card of some suit");
        }
        return builder.ToString();
    }


    public int CompareTo(CardProperties card2)
    {
        return value - card2.GetValue();
    }
}
