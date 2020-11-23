using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Transform target;

    //Declaring public variables

    //This is the target that is sought from the project
    public string targetName = "Head";
    //moves the object equal to 1*value each frame e.q. 0.125 = 1/8, 0.5 = 1/2
    public float smoothSpeed = 0.25f;
    //How far from the target the camera is desired to be, irrelevant if Inherit Transform is true
    public Vector3 offset;
    //Takes the current transform of the object and sets it as the offset
    public bool inheritTransform = false;

    //Declaring private variables

    //Where we want the camera to be
    private Vector3 desiredPosition;
    //private Vector3 smoothedPosition;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (inheritTransform == true) offset = transform.position;
        //sets the target as given targetname(default Head)
        target = FindTarget(targetName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Where the camera needs to be in order to follow the player, offset is added so the camera isn't inside the player
        desiredPosition = target.position + offset;

        //Camera jitter caused by conflict in player moving (fixed by putting tail movement in fixed update)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        //Turns the camera to look directly at the player
        //transform.LookAt(target);
        //transform.position = smoothedPosition;
    }

    //Takes the gameobject name and returns the positional value of the target that has the given name
    Transform FindTarget(string name)
    {
        GameObject lookAtTarget;
        //Finds the targets gameobject by its name
        lookAtTarget = GameObject.Find(name);
        //Sets the target value as the position of the gameobject that was searched
        target = lookAtTarget.GetComponent<Transform>();
        //Returns the value
        return target;
    }
}
