using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
	Vector3 angle;
    public float speed = 100;

	void Start()
	{
		angle = transform.eulerAngles;
	}

	void Update()
	{
        angle.y += Time.deltaTime * speed;
		transform.eulerAngles = angle;
        transform.position = new Vector3(transform.position.x, (Mathf.Sin(Time.time)/10), transform.position.z);
	}

}
