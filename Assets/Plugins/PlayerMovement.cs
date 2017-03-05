using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public Rigidbody rb;
    // Use this for initialization
    void FixedUpdate()
    {
        rb.AddForce(0, 0, 2000 * Time.deltaTime);   // Add a force of 2000 on the z-axis
    }
}