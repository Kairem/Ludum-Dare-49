using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunGravity : MonoBehaviour
{
    //Force of gravity = m1 * m2
    //                   -------
    //                     d^2
    public float massOfSun;
    public Transform centerOfTheSun;
    public float massOfPlayer;
    public float gravityConstant;
    float force;
    float distance;
    Vector3 forceDirection;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        massOfPlayer = rb.mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        distance = Vector3.Distance(centerOfTheSun.transform.position, transform.position);
        print("Distance: " + distance);
        force = gravityConstant * (massOfSun * massOfPlayer) / (distance * distance);
        print("Force: " + force);
        ApplyForce();
    }

    void ApplyForce()
    {
        if (distance < 70.0) {
            forceDirection = (centerOfTheSun.position - transform.position).normalized;
            rb.AddForce(force * forceDirection);
        }
        
    }
}
