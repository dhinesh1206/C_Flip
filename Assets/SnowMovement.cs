using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SnowMovement : MonoBehaviour {

    public float movementTime = 20;
    public Vector3 movementDifferencePosition = new Vector3(0,0.2f,0);

	void Start ()
    {
        StartAnimating();	
	}

    void StartAnimating()
    {
        gameObject.transform.DOLocalMove(movementDifferencePosition, movementTime).SetEase(Ease.Linear);    
    }
}
