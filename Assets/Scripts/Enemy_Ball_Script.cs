using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ball_Script : MonoBehaviour {

    public GameObject Manager;
    [HideInInspector]
    public Vector3 init_vel;

    // Use this for initialization
    void Start () {
        init_vel = GetComponent<Rigidbody>().velocity;

    }
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<Rigidbody>().velocity = init_vel;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            Manager.gameObject.GetComponent<Throw_balls>().GameOver();
            Debug.Log("Game Over");
        }

        if (col.gameObject.tag == "Player_Ball")
        {
            Manager.GetComponent<Throw_balls>().Delete_Ball(col.gameObject);
            Manager.GetComponent<Throw_balls>().Delete_Ball(gameObject);
            Manager.GetComponent<Throw_balls>().count++;
            Debug.Log("Balls collision");
        }

        if (col.gameObject.tag == "Wall")
        {
            Manager.GetComponent<Throw_balls>().Delete_Ball(gameObject);
            Debug.Log("Delete enemy ball");
        }
    }
}
