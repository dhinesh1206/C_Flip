using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour {

   GameObject targetPosition;

	// Use this for initialization
	void Start () {
        targetPosition = gameObject.transform.parent.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y > targetPosition.transform.position.y)
        {
            gameObject.SetActive(false);
        } 
	}
}
