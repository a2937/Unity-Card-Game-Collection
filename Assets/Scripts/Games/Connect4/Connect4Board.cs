using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect4Board : MonoBehaviour
{
    [SerializeField]
    private static int columnAmount = 15; 

    [SerializeField]
    private Connect4Column[] columns = new Connect4Column[columnAmount];

    private Player playerOne;
    private Player playerTwo; 

    // Use this for initialization
    void Start ()
    {
        
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
