using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant_Velocity : MonoBehaviour {

    Vector3 vel;

	// Use this for initialization
	void Start () {
        vel = GetComponent<Rigidbody>().velocity;

    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().velocity = vel;
    }
}
