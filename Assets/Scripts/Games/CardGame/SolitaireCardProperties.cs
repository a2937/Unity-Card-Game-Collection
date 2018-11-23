using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SolitaireCardProperties : CardProperties
{

    private Color color; //The color of the card

    [SerializeField]
    private Suit suit;


    public SolitaireCardProperties(int value, Suit suit, string cardName)
    {
        SetValue(value);
        SetSuit(suit);
        SetName(cardName);
    }


    /**
     * @return the suit
     */
    public Suit GetSuit()
    {
        return suit;
    }

    /**
     * @param suit the suit to set
     */
    public void SetSuit(Suit suit)
    {
        this.suit = suit;
        if (suit == Suit.HEARTS || suit == Suit.DIAMONDS)
        {
            this.color = Color.Red;
        }
        else
        {
            this.color = Color.Black;
        }
    }

    /**
     * @return If it is a black card
     */
    public bool IsBlack()
    {
        return color == Color.Black;
    }

    public override String GetSpriteName()
    {
        if (IsVisible())
        {
            return GetName() + GetSuit().ToString();
        }
        else
        {
            return "back";
        }
    }

    public override String ToString()
    {
        StringBuilder builder = new StringBuilder();
        if (IsVisible())
        {
            builder.Append(GetName()).Append(" of ").Append(GetSuit());
        }

        else
        {
            builder.Append("Some card of some suit");
        }
        return builder.ToString();
    }



}
