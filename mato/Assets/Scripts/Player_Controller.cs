using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;


public class Player_Controller : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 45f;
    Vector3 forward, right;

    public GameObject BodyPartObject;

    Vector3 rightMovement;
    Vector3 upMovement;

    Quaternion tresholdRotation;

    private float horizontalInput;
    private float verticalInput;


    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        tresholdRotation = Quaternion.Euler(0, 100, 0);
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime; // Go forward at all times.

        if (Mathf.Abs(verticalInput) > 0.25 || Mathf.Abs(horizontalInput) > 0.25)
        {
            Movement();
        }
    }

    void Movement()
    {
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * horizontalInput;   
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * verticalInput;
        Vector3 heading = new Vector3((rightMovement.x + upMovement.x), 0, (rightMovement.z + upMovement.z));
        Quaternion rotation;

        if (heading == Vector3.zero)
        {
            return;
        }
        else
        {
            rotation = Quaternion.LookRotation(heading, Vector3.up);
            transform.rotation = rotation;
        }
    }

    private void OnDrawGizmos()
    {
        rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        Vector3 heading = new Vector3((rightMovement.x + upMovement.x), 0, (rightMovement.z + upMovement.z));

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, heading);
    }

    void OnTriggerEnter(Collider other)
    {
        // Snack
        if (other.gameObject.CompareTag("Snack"))
        {
            BodyPartObject.GetComponent<Player_GrowScript>().Grow();
            Destroy(other.gameObject);
        }
    }
}