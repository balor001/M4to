using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawn_Fixed : MonoBehaviour
{
    public GameObject[] objects;                // The prefab to be spawned.
    public float spawnTime = 6f;            // How long between each spawn.
    

    // Use this for initialization
    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("Spawn", spawnTime, spawnTime);

    }

    void Spawn()
    {
        Instantiate(objects[UnityEngine.Random.Range(0, 1)], transform.position, Quaternion.identity);
    }
}
