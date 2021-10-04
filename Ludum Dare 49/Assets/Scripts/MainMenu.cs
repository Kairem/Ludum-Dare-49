using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera cam;
    public GameObject[] layers;
    public float parallaxSpeed = 10f;
   
    void Update()
    {
        layers[0].transform.Rotate(Vector3.forward * -parallaxSpeed * Time.deltaTime);
        layers[1].transform.Rotate(Vector3.forward * (parallaxSpeed-3f) * Time.deltaTime);
        layers[2].transform.Rotate(Vector3.forward * (parallaxSpeed-5f) * Time.deltaTime);
    }
}
