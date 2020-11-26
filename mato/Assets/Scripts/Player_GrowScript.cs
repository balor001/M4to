using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_GrowScript : MonoBehaviour
{
    [SerializeField]
    private List<Transform> bodyParts; // GameObject
    public int matoSize;
    private float zPos;

    public GameObject bodyPartPrefab;
    public GameObject tailPart;
    public GameObject headPart;
    public GameObject turretPart;

    public float RotThreshold = 1; //Thresholds to stop moving
    public float DisThreshold = 1.25f;
    private float dis;

    public float moveSpeed = 10f; //speed values to rotate and move
    public float rotSpeed = 4f;


    Vector3 TargetDirection;
    float Distance; //distance between the object and the target 

    Transform prevbPart;
    Transform currentbPart;

    // Start is called before the first frame update
    void Start()
    {
       // bodyParts = new List<GameObject>();
        bodyParts = new List<Transform>();
       for (int i = 0; i < matoSize; i++)
       {
           Grow();
       }
    }

    private void FixedUpdate()
    {
        // Check that list isn't null
        if (bodyParts != null)
        {
            // When there's no bodyparts, tail follows the head
            if (bodyParts.Count == 0)
            {
                Follow(turretPart, headPart); // turret follows head
                Follow(tailPart, turretPart); // tail follows head
            }

            // When Mato is larger than 0
            else if (bodyParts.Count > 0)
            {
                Follow(turretPart, headPart); // turret follows head
                // Iterate through every bodypart
                for (int i = 0; i < bodyParts.Count; i++)
                {

                    // first bodypart follows the head
                    if (i == 0)
                    {
                        Follow(bodyParts[i].gameObject, turretPart);
                    }

                    // Other bodyparts follow the next bodypart in the list
                    else
                    {
                        Follow(bodyParts[i].gameObject, bodyParts[i - 1].gameObject);
                    }
                }

                // tail follows the last bodypart in the list
                Follow(tailPart, bodyParts.Last().gameObject);
            }
        }
    }

    public void Follow(GameObject @object, GameObject target)
    {
        Distance = Vector3.Distance(@object.transform.position, target.gameObject.transform.position); //distance between target and object

        TargetDirection = Vector3.Scale((target.transform.position - @object.transform.position), new Vector3(1, 0, 1).normalized);

        // Rotation, Rotate towards target
        Vector3 forward = Vector3.Scale(@object.transform.forward, new Vector3(1, 0, 1)); //forward vector of current object
        Vector3 forwardUp = TargetDirection; //target rotation 
        Quaternion newRotation = Quaternion.FromToRotation(forward, forwardUp); //rotation calculated from current forward direction to target object's direction

        Vector3 newpos = @object.transform.position;

        if (newRotation.eulerAngles.magnitude > RotThreshold) //If the needed rotation bigger than the threshold object rotates
        {
            @object.transform.rotation = Quaternion.Lerp(@object.transform.rotation, @object.transform.rotation * newRotation, rotSpeed * Time.deltaTime); //rotation done from current to target rotation
        }

        // Move toward until distance treshold is met
        if (Distance > DisThreshold)
        {
            @object.transform.position = Vector3.Lerp(@object.transform.position, @object.transform.position + TargetDirection, moveSpeed * Time.deltaTime);
        }
    }

    public void Grow()
    {

        zPos = zPos - 1.25f; // Sets position

        // Maton BodyPartit pitäisi pienentää 0.7 - 0.99
        // ...

        Transform bPart = Instantiate(bodyPartPrefab.transform); // Instantiate new bodypart
        bPart.transform.position = tailPart.transform.position;
        bodyParts.Add(bPart); // Add the new Instantiated object to list
        bPart.transform.parent = transform; // Instantiate it as a parent of this object
        //tailPart.transform.rotation = new Quaternion(0, 0, 0, 0); // Zeros the rotation
        bPart.transform.rotation = new Quaternion(0, 0, 0, 0); // Zeros the rotation
        //bPart.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1); // Scales down the object
        bPart.name = "BodyPart_" + bodyParts.Count;

        tailPart.transform.position = tailPart.transform.position + new Vector3(0, 0, zPos).normalized;
    }


}
