using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : MonoBehaviour, ICardStackUIComponent
{
    private FoundationProperties foundationProperties;

    private SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetCardStack(ICardStack stack)
    {
        if(stack is FoundationProperties)
        {
            foundationProperties = stack as FoundationProperties;
        }
        
    }

    public ICardStack GetCardStack()
    {
        return foundationProperties; 
    }

    public void UpdateSprite()
    {
        if(foundationProperties.IsEmpty())
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(foundationProperties.GetSuit().ToString());

        }
        else
        {
            SolitaireCardProperties card = foundationProperties.Peek();
            spriteRenderer.sprite = Resources.Load<Sprite>(card.GetSpriteName()); 
        }
    }


    void Reset()
    {
        //Output the message to the Console
        Debug.Log("Reset");
        if (GetComponent<SpriteRenderer>() == null)
        {
            this.gameObject.AddComponent<SpriteRenderer>();
        }

    }
}
