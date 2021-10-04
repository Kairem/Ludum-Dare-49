using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    public GameObject introDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("enter") || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown("return"))
        {
            if (introDisplay != null)
            {
                introDisplay.SetActive(false);
            }
        }
    }
}
