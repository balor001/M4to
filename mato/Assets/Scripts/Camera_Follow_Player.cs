using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_Player : MonoBehaviour
{
    GameObject lookAtTarget;

    Transform target;

    //Declaring public variables
    public string targetName = "Head";
    public float smoothSpeed = 0.25f;
    public Vector3 offset;
    public bool inheritTransform = false;

    //Declaring private variables
    Vector3 desiredPosition;
    Vector3 smoothedPosition;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        if (inheritTransform == true) offset = transform.position;
        //sets the target as given targetname(default Head)
        target = FindTarget(targetName);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        desiredPosition = target.position + offset;

        //Camera jitter caused by conflict in player moving (fixed by putting tail movement in fixed update)
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        transform.LookAt(target);
        //transform.position = smoothedPosition;
    }
    //https://youtu.be/MFQhpwc6cKE?t=231

    //Takes the gameobject name and returns the value to look at the target that has the given name
    Transform FindTarget(string name)
    {
        //Finds the targets gameobject by its name
        lookAtTarget = GameObject.Find(name);
        //Sets the target value as the position of the gameobject that was searched
        target = lookAtTarget.GetComponent<Transform>();
        //Returns the value
        return target;
    }
}
