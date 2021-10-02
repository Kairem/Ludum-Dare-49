using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Camera cam;
    public GameObject[] layers;
    //Boundary
    private float minX = -50;
    private float maxX = 50;
    private float minY = -50;
    private float maxY = 50;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        //Boundary
        float newPosX;
        float newPosY;
        newPosX = Mathf.Clamp(transform.position.x, minX, maxX);
        newPosY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(newPosX, newPosY, 0);
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position;
        cam.transform.position += new Vector3(0, 0, -5);

        layers[0].transform.position = new Vector3(-cam.transform.position.x * 0.75f, -cam.transform.position.y * 0.75f, 0);
        layers[1].transform.position = new Vector3(-cam.transform.position.x * 0.5f, -cam.transform.position.y * 0.50f, 0);
        layers[2].transform.position = new Vector3(-cam.transform.position.x * 0.25f, -cam.transform.position.y * 0.25f, 0);

    }

}
