using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableObjects : MonoBehaviour {

    public bool objectState;
    public GameObject objecttoActivateOrDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "JumpingCube")
        {
            // print(Camera.main.transform.position);
            //print(Camera.main.transform.eulerAngles);
            objecttoActivateOrDeactivate.transform.position = Camera.main.transform.position;
            objecttoActivateOrDeactivate.transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles);
            objecttoActivateOrDeactivate.SetActive(objectState);
        }
    }
}
