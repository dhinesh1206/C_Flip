using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFallingObjects : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PickUpObjects")
        {
            ScoreManagement scoreManagement = other.gameObject.GetComponent<ScoreManagement>();
            if (!scoreManagement.added)
            {
                GameManager.instance.ObjectPointIncrement(other.gameObject.GetComponent<ScoreManagement>().objectScoreType, other.gameObject.transform.position, other.gameObject.GetComponent<ScoreManagement>().textColour);
            }
        }
    }
}
