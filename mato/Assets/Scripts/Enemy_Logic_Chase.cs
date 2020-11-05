using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Logic_Chase : MonoBehaviour
{

    public Transform Player;
    public int MoveSpeed = 4;
    public int NoFurtherThan = 10;
    public int HowClose = 0;


    void FindPlayer()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) FindPlayer();

        //Move towards player
        //
        //Look at player
        transform.LookAt(Player);

        //Move forward
        if (Vector3.Distance(transform.position, Player.position) >= HowClose)
        {

            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, Player.position) <= NoFurtherThan)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
    }
}


