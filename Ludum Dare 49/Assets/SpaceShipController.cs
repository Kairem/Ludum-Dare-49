using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    // Initial Variables
    public float rotationDegreePerSec = 180f;
    public float force = 20.0f;
    public Rigidbody2D rb;
    
    private bool isThrusting;
    private float turnDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 1.5f;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    // physics calculations
    {
        Move();
    }

    void ProcessInputs()
    {
        isThrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        } else
        {
            turnDirection = 0.0f;
        }
    }

    void Move()
    {
        if (isThrusting)
        {
            rb.AddForce(force * gameObject.transform.up);
        }

        if (turnDirection != 0.0f)
        {
            rb.angularVelocity = turnDirection * rotationDegreePerSec;
        } else
        {
            rb.angularVelocity = 0;
        }
    }
}
