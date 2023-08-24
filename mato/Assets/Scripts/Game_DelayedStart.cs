using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_DelayedStart : MonoBehaviour
{
    
    Player_Controller player_Controller;
    // Start is called before the first frame update
    void Start()
    {
        //Sets the starting text
        GetComponent<TMPro.TextMeshProUGUI>().text = "3";
        //Seeks the player controller in order to manipulate its movement
        player_Controller = FindObjectOfType<Player_Controller>();
        //Starts a side operation that has all of the functionalities
        StartCoroutine(WaitForSceneLoad());
    }
    // Update is called once per frame
    private IEnumerator WaitForSceneLoad()
    {
        try
        {
            //Sets player maneuverability off 
            player_Controller.activeControls = false;
        }
        catch (Exception e)
        {
            print("NoPlayerController");
        }


        //Waits a second, then changes text, repeats until GO
        yield return new WaitForSeconds(1);
        GetComponent<TMPro.TextMeshProUGUI>().text = "2";
        yield return new WaitForSeconds(1);
        GetComponent<TMPro.TextMeshProUGUI>().text = "1";
        yield return new WaitForSeconds(1);
        GetComponent<TMPro.TextMeshProUGUI>().text = "GO";
        GetComponent<TMPro.TextMeshProUGUI>().fontSize = 200;

        try
        {
            //Sets player control on
            player_Controller.activeControls = true;
        }
        catch (Exception e)
        {
            print("NoPlayerController");
        }

        //Leaves Go on screen
        yield return new WaitForSeconds(1);

        //Shuts off the UI element
        gameObject.SetActive(false);

    }
}
