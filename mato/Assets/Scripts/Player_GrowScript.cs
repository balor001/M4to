using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_GrowScript : MonoBehaviour
{
    private List<GameObject> matoBodyParts;
    private float zPos;
    private float scaleMultiplier = 1;

    public GameObject bodyPart;
    public GameObject tailPart;
    public GameObject headPart;
    GameObject previousBodyPart;

    public float RotThreshold = 1; //Thresholds to stop moving
    public float DisThreshold = 1.25f;

    public float moveSpeed = 4f; //speed values to rotate and move
    public float rotSpeed = 4f;

    Vector3 TargetDirection;
    float Distance; //distance between the object and the target 

    // Start is called before the first frame update
    void Start()
    {
        matoBodyParts = new List<GameObject>();
    }

    private void Update()
    {
        Debug.Log(matoBodyParts.Count);
        Follow(bodyPart, headPart);
        Follow(tailPart, bodyPart);

    }

    void Follow(GameObject @object, GameObject target)
    {
        Distance = Vector3.Distance(@object.transform.position, target.gameObject.transform.position); //distance between target and object

        TargetDirection = Vector3.Scale((target.transform.position - @object.transform.position), new Vector3(1,0,1).normalized);

        // Rotation, Rotate towards target
        Vector3 forward = Vector3.Scale(@object.transform.forward, new Vector3(1, 0, 1)); //forward vector of current object
        Vector3 forwardUp = TargetDirection; //target rotation 
        Quaternion newRotation = Quaternion.FromToRotation(forward, forwardUp); //rotation calculated from current forward direction to target object's direction
        if (newRotation.eulerAngles.magnitude > RotThreshold) //If the needed rotation bigger than the threshold object rotates
        {
            @object.transform.rotation = Quaternion.Lerp(@object.transform.rotation, @object.transform.rotation * newRotation, rotSpeed * Time.deltaTime); //rotation done from current to target rotation
        }

        // Move
        if (Distance > DisThreshold)
        {
            @object.transform.position = Vector3.Lerp(@object.transform.position, @object.transform.position + TargetDirection, moveSpeed * Time.deltaTime);
        }

    }


    public void Grow()
    {
        matoBodyParts.Add(bodyPart);

        zPos = zPos - 1.25f;

        // Maton BodyPartit pitäisi pienentää 0.7 - 0.99

        GameObject bPart = Instantiate(bodyPart);
        bPart.transform.parent = transform;
        bPart.transform.localPosition = new Vector3(0, 0, zPos);
        bPart.transform.rotation = new Quaternion(0, 0, 0, 0);
        bPart.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
        bPart.name = "BodyPart_" + matoBodyParts.Count;

        tailPart.transform.parent = transform;
        tailPart.transform.localPosition = new Vector3(0, 0, zPos - 1.5f);
    }
}
