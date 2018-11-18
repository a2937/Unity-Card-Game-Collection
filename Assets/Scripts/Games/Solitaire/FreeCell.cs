using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCell : MonoBehaviour, ICardStackUIComponent
{
    private SpriteRenderer spriteRenderer;
    private FreeCellProperties freeCell; 

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    

    public void UpdateSprite()
    {
        if (freeCell.IsEmpty())
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("Generic");

        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>(freeCell.Peek().GetSpriteName());
        }
    }

    public ICardStack GetCardStack()
    {
        return freeCell;
    }

    public void SetCardStack(ICardStack stack)
    {
        if (stack is FreeCellProperties)
        {
            freeCell = stack as FreeCellProperties;
            UpdateSprite();
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
