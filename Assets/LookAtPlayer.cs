using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {
    [HideInInspector]
    public bool lookAt;
	
	// Update is called once per frame
	void Update ()
    {
	    if(lookAt)
        {
            if(GameManager.instance.playerInLevel.GetComponent<FollowCamera>())
            {
                GameManager.instance.playerInLevel.GetComponent<FollowCamera>().enabled = false;
            }
            transform.LookAt(GameManager.instance.playerInLevel.transform);
        }
	}

}
