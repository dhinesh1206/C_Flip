using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public static FollowCamera instance;
    //public GameObject objectTofollow;
    public Vector3 offset;
    bool startFollowing;

    private void Awake()
    {
        instance = this;
    }

    void Start () {
        //Invoke("StartFollowing", 1.5f);
	}
	
	void Update () 
    {
        if (startFollowing)
        {
            GameManager.instance.mainCam.transform.position = transform.position-offset;
        }
	}

    public void StartFollowing()
    {
        startFollowing = true;
        offset = (transform.position - GameManager.instance.mainCam.transform.position);
    }

    public void StopFollowing()
    {
        startFollowing = false;
    }
}
