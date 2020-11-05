using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicker : MonoBehaviour
{
    void Update()
    {
        //This only plays the clicking sound in scene when the button is clicked
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<AudioManager>().Play("ClickSound");
        }
    }
}
