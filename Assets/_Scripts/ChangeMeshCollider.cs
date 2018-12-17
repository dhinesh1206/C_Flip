using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMeshCollider : MonoBehaviour {

    MeshCollider msc;

	// Use this for initialization
	void Start () {
        msc = gameObject.GetComponent<MeshCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        msc.sharedMesh = null;
        msc.sharedMesh = gameObject.GetComponent<SkinnedMeshRenderer>().sharedMesh;
	}
}
