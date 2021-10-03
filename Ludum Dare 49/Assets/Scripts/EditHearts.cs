using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditHearts : MonoBehaviour
{
    public GameObject[] uiHearts;
    private Image image;

    public void UpdateGraphic(int hearts)
    {
        if (hearts == 2)
        {
            print("first heart");
            image = uiHearts[0].GetComponent<Image>();
            var tempColor = image.color;
            print(tempColor);
            tempColor.a = 0.3f;
            image.color = tempColor;
        }
        else if (hearts == 1)
        {
            image = uiHearts[1].GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 0.3f;
            image.color = tempColor;
        }
        else if (hearts <= 0)
        {
            print("all hearts");
            for (int i=0; i<3; i++)
            {
                image = uiHearts[i].GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 0.3f;
                image.color = tempColor;
            }
            
        }
    }
}
