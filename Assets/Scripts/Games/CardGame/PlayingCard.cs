using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[Serializable]
public class PlayingCard : MonoBehaviour, ICustomUIComponent
{
  

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private SolitaireCardProperties properties;

    
    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCardProperties(SolitaireCardProperties props)
    {
        this.properties = props;
        UpdateSprite(); 
    }

  
    public void UpdateSprite()
    {
        if(properties == null)
        {
            spriteRenderer.sprite = null; 
        }
        else
        {
            if (properties.IsVisible())
            {
                this.spriteRenderer.sprite = Resources.Load<Sprite>(this.properties.GetSpriteName());
            }
            else
            {
                this.spriteRenderer.sprite = Resources.Load<Sprite>("back");
            }
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
