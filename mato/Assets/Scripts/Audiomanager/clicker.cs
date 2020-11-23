using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clicker : MonoBehaviour
{
    AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        //This is the trigger, irrelevant atm
        if (Input.GetButtonDown("Fire1"))
        {
            //The sound has to be exactly the sound to be played
            audioManager.Play("Sound");
        }
    }
}
