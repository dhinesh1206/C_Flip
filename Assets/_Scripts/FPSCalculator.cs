using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSCalculator : MonoBehaviour {

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Update ()
    {
        gameObject.GetComponent<Text>().text = (1 / Time.deltaTime).ToString();
	}
}
