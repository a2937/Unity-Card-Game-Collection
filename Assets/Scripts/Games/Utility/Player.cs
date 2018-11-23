using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField]
    private String name;

    [SerializeField]
    protected float stash;

    [SerializeField]
    private Color color; //Used for Connect4

    [SerializeField]
    private List<SolitaireCardProperties> hand = new List<SolitaireCardProperties>();

    private const int STRAIGHT_FLUSH = 8000000;
    // + valueHighCard()
    private const int FOUR_OF_A_KIND = 7000000;
    // + Quads Card Rank
    private const int FULL_HOUSE = 6000000;
    // + SET card rank
    private const int FLUSH = 5000000;
    // + valueHighCard()
    private const int STRAIGHT = 4000000;
    // + valueHighCard()
    private const int SET = 3000000;
    // + Set card value
    private const int TWO_PAIRS = 2000000;
    // + High2*14^4+ Low2*14^2 + card
    private const int ONE_PAIR = 1000000;
    // + high*14^2 + high2*14^1 + low

    public Player(String name, float stash2)
    {
        SetName(name);
        SetStash(stash2);
    }

    public Player(String name, Color color)
    {
        SetName(name);
        SetColor(color); 
    }


    public Player()
    {
        SetName("Player");
        SetStash(500); 
    }

    public int Bet(int currentBet)
    {
        if ((currentBet > stash) || (currentBet < 0))
        {
            return -1;
        }
        stash -= currentBet;
        return currentBet;
    }

    public Color GetColor()
    {
        return color; 
    }

    public void SetColor(Color color)
    {
        this.color = color; 
    }

    public void SetCardVisible()
    {
        SolitaireCardProperties card = hand.Get(0);
        card.Show();
        hand.Set(0, card);
    }

    public void SetCardVisible(int index)
    {
        SolitaireCardProperties card = hand.Get(index);
        card.Show();
        hand.Set(index, card);
    }

    /**
     * 
     * @param theNameLabel : The label the graphical application provided
     * @param deck : the array of labels representing the cards
     */
     /*
    public void fold(JLabel theNameLabel, JLabel[] deck)
    {
        for (int i = 0; i < hand.size(); i++)
        {
            setCardVisible(i);
        }

        this.toImage(theNameLabel, deck);
        hand.clear();
        hand.trimToSize();
    }
    */ 


    private void SortByValue()
    {
        int smallest;
        SolitaireCardProperties tempCard;

        for (int current = 0; current < hand.Count - 1; current++)
        {
            smallest = current;
            tempCard = hand.Get(current);
            for (int walk = current + 1; walk <= hand.Count - 1; walk++)
            {
                if (walk != 0 && hand.Get(walk - 1).CompareTo(tempCard) > 0)
                {
                    smallest = walk;
                }
            }

            tempCard = hand.Get(current);

            hand.Set(current, hand.Get(smallest));

            hand.Set(smallest, tempCard);
        }

    }

    private void SortBySuit()
    {
        int smallest;
        SolitaireCardProperties tempCard;

        for (int current = 0; current < hand.Count; current++)
        {
            smallest = current;
            tempCard = hand.Get(current);
            for (int walk = current + 1; walk <= hand.Count - 1; walk++)
            {
                if (walk != 0 && hand.Get(walk - 1).GetSuit() - tempCard.GetSuit() > 0)
                {
                    smallest = walk;
                }
            }

            tempCard = hand.Get(current);

            hand.Set(current, hand.Get(smallest));

            hand.Set(smallest, tempCard);
        }
    }

    /**
     * @return the name
     */
    public String GetName()
    {
        return name;
    }

    /**
     * @param newName the name to set
     * If the player does not provide a name,
     * their name Gets set to Player
     */
    public void SetName(String newName)
    {
        if (!newName.Equals(""))
        {
            this.name = newName;
        }
        else
        {
            this.name = "Player";
        }
    }

    /**
     * @return the stash
     */
    public float GetStash()
    {
        return stash;
    }

    /**
     * Text version
     */
    public void Fold()
    {
        for (int i = 0; i < hand.Count; i++)
        {
            SetCardVisible(i);
        }

        hand.Clear();
       
    }

    /**
     * @param stash2 :the stash to set
     * If they don`t have any money, 
     * the house will let them have fifty dollars
     * to Get them started. 
     * 
     */
    public void SetStash(float stash2)
    {
        if (stash2 > 1)
        {
            this.stash = stash2;
        }

        else
        {
          
            this.stash = 50;
        }
    }

    public int GetHandSize()
    {
        return hand.Count;
    }

    public void AddCard(SolitaireCardProperties card)
    {
        if (hand.Count < 5)
        {
            hand.Add(card);
        }
    }

    public override String ToString()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("Player`s name: ").Append(GetName()).Append("\n");
        builder.Append("Their hand: ");
        for (int i = 0; i <= hand.Count - 2; i++)
        {
            builder.Append(hand.Get(i).ToString()).Append(",").Append(" ");
        }
        builder.Append(hand.Get(hand.Count - 1).ToString());

        builder.Append("\n");
        builder.Append("Amount of money: ").Append(GetStash());
        return builder.ToString();
    }


    public List<SolitaireCardProperties> GetHand()
    {
        List<SolitaireCardProperties> cards = new List<SolitaireCardProperties>();
        for(int i = 0; i < hand.Count; i++)
        {
            cards.Add(hand.Get(i));
        }
        return cards; 
    }

    /*
     * Returns the sum of cards in the players hand.
     * If it contains an Ace it will either score the ace as 11 
     * or as a one depending on if the total sum of the hand can handle an
     * 11 without going over. 
     */
    public int ScoreBlackJackHand()
    {
        bool containsAce = false;
        int aceIndex = -1; 

        foreach(SolitaireCardProperties card in hand)
        {
            if(card.GetName().Equals("Ace"))
            {
                containsAce = true;
                aceIndex = hand.IndexOf(card); 
                break; 
            }
        }

        if(containsAce)
        {
            int sum = 0; 
            for(int i = 0; i < hand.Count; i++)
            {
                if(i != aceIndex)
                {
                    sum += hand.Get(i).GetValue();
                }
            }

            if(sum + 11 <= 21)
            {
                sum += 11; 
            }
            else
            {
                sum++; 
            }

            return sum; 
        }
        else
        {
            int sum = 0;
            foreach (SolitaireCardProperties card in hand)
            {
                sum += card.GetValue();
            }
            return sum; 
        }


    }


    /***********************************************************
    Methods used to determine a certain Poker hand
   ***********************************************************/

    /* --------------------------------------------------------
       scoreHand(): return score of a hand
       -------------------------------------------------------- */
    public int ScoreHand()
    {
        if (IsFlush() && IsStraight())
        {
            return ScoreStraightFlush();
        }

        else if (Is4s())
        {
            return ScoreFourOfAKind();
        }

        else if (IsFullHouse())
        {
            return ScoreFullHouse();
        }
        else if (IsFlush())
        {
            return ScoreFlush();
        }
        else if (IsStraight())
        {
            return ScoreStraight();
        }

        else if (Is3s())
        {
            return ScoreSet();
        }

        else if (Is22s())
        {
            return ScoreTwoPairs();
        }
        else if (Is2s())
        {
            return ScoreOnePair();
        }
        else
        {
            return ScoreHighCard();
        }

    }

    /* -----------------------------------------------------
       scoreFlush(): return score of a Flush hand

             score = FLUSH + scoreHighCard()
       ----------------------------------------------------- */
    public int ScoreStraightFlush()
    {
        return STRAIGHT_FLUSH + ScoreHighCard();
    }

    /* -----------------------------------------------------
       scoreFlush(): return score of a Flush hand

             score = FLUSH + scoreHighCard()
       ----------------------------------------------------- */
    public int ScoreFlush()
    {
        return FLUSH + ScoreHighCard();
    }

    /* -----------------------------------------------------
       scoreStraight(): return score of a Straight hand

             score = STRAIGHT + scoreHighCard()
       ----------------------------------------------------- */
    public int ScoreStraight()
    {
        return STRAIGHT + ScoreHighCard();
    }

    /* ---------------------------------------------------------
       scoreFourOfAKind(): return score of a 4 of a kind hand

             score = FOUR_OF_A_KIND + 4sCardRank

       Trick: card hand.Get(2] is always a card that is part of 
              the 4-of-a-kind hand
           There is ONLY ONE hand with a quads of a given rank.
       --------------------------------------------------------- */
    public int ScoreFourOfAKind()
    {
        SortByValue();

        return FOUR_OF_A_KIND + hand.Get(2).GetValue();
    }

    /* -----------------------------------------------------------
       scoreFullHouse(): return score of a Full House hand

             score = FULL_HOUSE + SetCardRank

       Trick: card hand.Get(2] is always a card that is part of
              the 3-of-a-kind in the full house hand
           There is ONLY ONE hand with a FH of a given set.
       ----------------------------------------------------------- */
    public int ScoreFullHouse()
    {
        SortByValue();

        return FULL_HOUSE + hand.Get(2).GetValue();
    }

    /* ---------------------------------------------------------------
       scoreSet(): return score of a Set hand

             score = SET + SetCardRank

       Trick: card hand.Get(2] is always a card that is part of the set hand
           There is ONLY ONE hand with a set of a given rank.
       --------------------------------------------------------------- */
    public int ScoreSet()
    {
        SortByValue();

        return SET + hand.Get(2).GetValue();
    }

    /* -----------------------------------------------------
       scoreTwoPairs(): return score of a Two-Pairs hand

             score = TWO_PAIRS
                    + 14*14*HighPairCard
                    + 14*LowPairCard
                    + UnmatchedCard
       ----------------------------------------------------- */
    public int ScoreTwoPairs()
    {
        int val = 0;

        SortByValue();

        if (hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
             hand.Get(2).GetValue() == hand.Get(3).GetValue())
        {
            val = 14 * 14 * hand.Get(2).GetValue() + 14 * hand.Get(0).GetValue() + hand.Get(4).GetValue();
        }
        else if (hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
                 hand.Get(3).GetValue() == hand.Get(4).GetValue())
        {
            val = 14 * 14 * hand.Get(3).GetValue() + 14 * hand.Get(0).GetValue() + hand.Get(2).GetValue();
        }
        else
        {
            val = 14 * 14 * hand.Get(3).GetValue() + 14 * hand.Get(1).GetValue() + hand.Get(0).GetValue();
        }

        return TWO_PAIRS + val;
    }

    /* -----------------------------------------------------
       scoreOnePair(): return score of a One-Pair hand

             score = ONE_PAIR 
                    + 14^3*PairCard
                    + 14^2*HighestCard
                    + 14*MiddleCard
                    + LowestCard
       ----------------------------------------------------- */
    public int ScoreOnePair()
    {
        int val = 0;

        SortByValue();

        if (hand.Get(0).GetValue() == hand.Get(1).GetValue())
        {
            val = (14 * 14 * 14 * hand.Get(0).GetValue()) +
                   +hand.Get(2).GetValue() + (14 * hand.Get(3).GetValue()) + (14 * 14 * hand.Get(4).GetValue());
        }
        else if (hand.Get(1).GetValue() == hand.Get(2).GetValue())
        {
            val = (14 * 14 * 14 * hand.Get(1).GetValue()) +
                  +hand.Get(0).GetValue() + 14 * hand.Get(3).GetValue() + (14 * 14 * hand.Get(4).GetValue());
        }
        else if (hand.Get(2).GetValue() == hand.Get(3).GetValue())
        {
            val = (14 * 14 * 14 * hand.Get(2).GetValue()) +
                  +hand.Get(0).GetValue() + 14 * hand.Get(1).GetValue() + (14 * 14 * hand.Get(4).GetValue());
        }
        else
        {
            val = (14 * 14 * 14 * hand.Get(3).GetValue()) +
                  +hand.Get(0).GetValue() + 14 * hand.Get(1).GetValue() + 14 * 14 * hand.Get(2).GetValue();
        }

        return ONE_PAIR + val;
    }

    /* -----------------------------------------------------
       scoreHighCard(): return score of a high card hand

             score =  14^4*highestCard 
                    + 14^3*2ndHighestCard
                    + 14^2*3rdHighestCard
                    + 14^1*4thHighestCard
                    + LowestCard
       ----------------------------------------------------- */
    public int ScoreHighCard()
    {
        int val;

        SortByValue();

        val = hand.Get(0).GetValue() + 14 * hand.Get(1).GetValue() + 14 * 14 * hand.Get(2).GetValue()
              + 14 * 14 * 14 * hand.Get(3).GetValue() + 14 * 14 * 14 * 14 * hand.Get(4).GetValue();

        return val;
    }


    /***********************************************************
      Methods used to determine a certain Poker hand
     ***********************************************************/


    /* ---------------------------------------------
       is4s(): true if h has 4 of a kind
               false otherwise
       --------------------------------------------- */
    public bool Is4s()
    {
        bool a1;
        bool a2;

        if (hand.Count != 5)
        {
            return (false);
        }

        SortByValue();

        a1 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
             hand.Get(1).GetValue() == hand.Get(2).GetValue() &&
             hand.Get(2).GetValue() == hand.Get(3).GetValue();

        a2 = hand.Get(1).GetValue() == hand.Get(2).GetValue() &&
             hand.Get(2).GetValue() == hand.Get(3).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        return (a1 || a2);
    }


    /* ----------------------------------------------------
       isFullHouse(): true if h has Full House
                      false otherwise
       ---------------------------------------------------- */
    public bool IsFullHouse()
    {
        bool a1;
        bool a2;

        if (hand.Count != 5)
        {
            return (false);
        }

        SortByValue();

        a1 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&  //  x x x y y
             hand.Get(1).GetValue() == hand.Get(2).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        a2 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&  //  x x y y y
             hand.Get(2).GetValue() == hand.Get(3).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        return (a1 || a2);
    }



    /* ----------------------------------------------------
       is3s(): true if h has 3 of a kind
               false otherwise

       **** Note: use is3s() ONLY if you know the hand
                  does not have 4 of a kind 
       ---------------------------------------------------- */
    public bool Is3s()
    {
        bool a1;
        bool a2;
        bool a3;


        if (hand.Count != 5)
        {
            return (false);
        }

        if (Is4s() || IsFullHouse())
        {
            return (false);        // The hand is not 3 of a kind (but better)
        }


        /* ----------------------------------------------------------
           Now we know the hand is not 4 of a kind or a full house !
           ---------------------------------------------------------- */
        SortByValue();

        a1 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
             hand.Get(1).GetValue() == hand.Get(2).GetValue();

        a2 = hand.Get(1).GetValue() == hand.Get(2).GetValue() &&
             hand.Get(2).GetValue() == hand.Get(3).GetValue();

        a3 = hand.Get(2).GetValue() == hand.Get(3).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        return (a1 || a2 || a3);
    }

    /* -----------------------------------------------------
       is22s(): true if h has 2 pairs
                false otherwise

       **** Note: use is22s() ONLY if you know the hand
                  does not have 3 of a kind or better
       ----------------------------------------------------- */
    public bool Is22s()
    {
        bool a1;
        bool a2;
        bool a3;


        if (hand.Count != 5)
        {
            return (false);
        }

        if (Is4s() || IsFullHouse() || Is3s())
        {
            return (false);        // The hand is not 2 pairs (but better)
        }


        SortByValue();

        a1 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
             hand.Get(2).GetValue() == hand.Get(3).GetValue();

        a2 = hand.Get(0).GetValue() == hand.Get(1).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        a3 = hand.Get(1).GetValue() == hand.Get(2).GetValue() &&
             hand.Get(3).GetValue() == hand.Get(4).GetValue();

        return (a1 || a2 || a3);
    }


    /* -----------------------------------------------------
       is2s(): true if h has one pair
               false otherwise

       **** Note: use is22s() ONLY if you know the hand
                  does not have 2 pairs or better
       ----------------------------------------------------- */
    public bool Is2s()
    {
        bool a1, a2, a3, a4;

        if (hand.Count != 5)
        {
            return (false);
        }

        if (Is4s() || IsFullHouse() || Is3s() || Is22s())
        {
            return (false);
        }
        // The hand is not one pair (but better)

        SortByValue();

        a1 = hand.Get(0).GetValue() == hand.Get(1).GetValue();
        a2 = hand.Get(1).GetValue() == hand.Get(2).GetValue();
        a3 = hand.Get(2).GetValue() == hand.Get(3).GetValue();
        a4 = hand.Get(3).GetValue() == hand.Get(4).GetValue();

        return (a1 || a2 || a3 || a4);
    }


    /* ---------------------------------------------
       isFlush(): true if h has a flush
                  false otherwise
       --------------------------------------------- */
    public bool IsFlush()
    {
        if (hand.Count != 5)
        {
            return (false);
        }

        SortBySuit();

        return (hand.Get(0).GetSuit() == hand.Get(4).GetSuit());   // All cards has same suit
    }


    /* ---------------------------------------------
       isStraight(): true if h is a Straight
                     false otherwise
       --------------------------------------------- */
    public bool IsStraight()
    {
        int i;
        int testValue;


        if (hand.Count != 5)
        {
            return (false);
        }

        SortByValue();

        /* ===========================
           Check if hand has an Ace
           =========================== */
        if (hand.Get(4).GetName() == "Ace")
        {
            /* =================================
               Check straight using an Ace
               ================================= */
            bool a = hand.Get(0).GetValue() == 2 && hand.Get(1).GetValue() == 3 &&
                        hand.Get(2).GetValue() == 4 && hand.Get(3).GetValue() == 5;
            bool b = hand.Get(0).GetValue() == 10 && hand.Get(1).GetValue() == 11 &&
                        hand.Get(2).GetValue() == 12 && hand.Get(3).GetValue() == 13;

            return (a || b);
        }
        else
        {
            /* ===========================================
               General case: check for increasing scores
               =========================================== */
            testValue = hand.Get(0).GetValue() + 1;

            for (i = 1; i < 5; i++)
            {
                if (hand.Get(i).GetValue() != testValue)
                {
                    return (false);        // Straight failed...
                }

                testValue++;
            }

            return (true);        // Straight found !
        }
    }
}
