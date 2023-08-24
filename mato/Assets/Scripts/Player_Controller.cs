using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Schema;
using UnityEngine;


public class Player_Controller : MonoBehaviour
{
    public bool activeControls = true;

    public float moveSpeed = 4f;
    public float rotationSpeed = 45f;

    Vector3 forward, right;

    public GameObject BodyPartObject;

    private float horizontalInput;
    private float verticalInput;

    private GameManager gameManager;
    private Joystick joystick;
    private AudioManager audioManager;

    Vector3 lastDirection;
    public float oppositeThreshhold = 0; // How precisely does the analog stick have to be pressed in the opposite direction it was previously?

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward; // Use cameras forward vector, makes forward movement of "north"
        forward.y = 0; // Ensure y-value is always zero (No up and down movement)
        forward = Vector3.Normalize(forward); // Normalizes forward vector, makes lenght of forward vector 1

        // 
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward; // Makes right vector basically -45 degrees from the world axis

        audioManager = FindObjectOfType<AudioManager>();
        gameManager = FindObjectOfType<GameManager>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") + joystick.Horizontal;
        verticalInput = Input.GetAxis("Vertical") + joystick.Vertical;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If controls are active
        if (activeControls)
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime; // Go forward at all times.

            // Joystick movement
            if (Mathf.Abs(verticalInput) > 0.25 || Mathf.Abs(horizontalInput) > 0.25)
            {
                Movement();
            }
        }
    }

    // Moves the Head object, or rather it rotates the object
    void Movement()
    {
        Vector3 rightMovement = right * horizontalInput; // Makes rightmovement align with the horizontal input in isometric view (West, east)
        Vector3 upMovement = forward * verticalInput;// Makes upmovement align with the vertical input in isometric view (north,south)
        Vector3 heading = new Vector3((rightMovement.x + upMovement.x), 0, (rightMovement.z + upMovement.z)); // Direction of the forward vector

        // If heading is zero, no joystick input.
        if (heading == Vector3.zero)
        {
            return;
        }
        // IF player turns directly opposite, ignore its input.
        if (Vector3.Dot(heading, lastDirection) < 0f)
        {
            return;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(heading, Vector3.up); // Rotates head towards direction of the movement or heading
            transform.rotation = rotation; // Rotates object
            lastDirection = heading;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Snack
        if (other.gameObject.CompareTag("Snack"))
        {
            if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
            BodyPartObject.GetComponent<Player_GrowScript>().Grow();
            other.gameObject.SetActive(false);
            audioManager.Play("EatSound");
            gameManager.pickUpCount++;
        }

        // When Colliding with itself
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.GameOver();
            activeControls = false;
        }

        // When colliding with a collidable object(s)
        if (other.gameObject.CompareTag("Collider"))
        {
            gameManager.GameOver();
            activeControls = false;
        }
    }
}