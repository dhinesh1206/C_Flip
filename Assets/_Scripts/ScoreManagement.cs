using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManagement : MonoBehaviour 
{
    public GameManager.AdditionalObjectsType objectScoreType;
    public bool added;
    public Color textColour = Color.black;

    private void Start()
    {
        transform.tag = "PickUpObjects";
    }
}
