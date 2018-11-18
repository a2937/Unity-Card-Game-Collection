using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Utility/List", order = 1)]
public class GameRules : ScriptableObject
{
    public string Title { get; set; }

    public string Rules { get; set; }

}
