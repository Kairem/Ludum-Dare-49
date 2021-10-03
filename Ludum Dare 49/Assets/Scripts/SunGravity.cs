using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SunGravity : MonoBehaviour
{
    public CircleCollider2D cc;
    public float massOfSun = 3000;
    public float gravityConstant = 1;
    List<Collider2D> listOfObjects;

    private void Start()
    {
        listOfObjects = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>()) {
            listOfObjects.Add(other);
            print(listOfObjects);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        for (var i = 0; i < listOfObjects.Count; i++)
        {
            if (listOfObjects[i] == other) {
                listOfObjects.RemoveAt(i);
                print(listOfObjects);
            }
        }
    }

    private void FixedUpdate()
    {
       
        foreach (Collider2D obj in listOfObjects) {
            ApplyForce(obj);
        }
        
    }

    void ApplyForce(Collider2D obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        float massOfObject = rb.mass;
        float distance;
        float force;
        Vector3 forceDirection;

        distance = Vector3.Distance(transform.position, obj.transform.position);
        force = gravityConstant * (massOfSun * massOfObject) / (distance * distance);
        forceDirection = (transform.position - obj.transform.position).normalized;
        rb.AddForce(force * forceDirection);
    }
}
