using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScoreCollider : MonoBehaviour 
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ScoringObjects")
        {
            if (!other.GetComponent<ScoreManagement>().added)
            {
                other.GetComponent<ScoreManagement>().added = true;
                GameManager.instance.ObjectPointIncrement(other.GetComponent<ScoreManagement>().objectScoreType, other.transform.position,other.GetComponent<ScoreManagement>().textColour);
            }
        }
    }
}
