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

    /*
    public GameObject TurretObject;
    public Transform gunEndPoint;
    [SerializeField] private Transform bullet;
    */

    public float fireRate = 2f;
    public float nextFire;
    public float bulletForce = 100f;

    Vector3 rightMovement;
    Vector3 upMovement;

    Quaternion tresholdRotation;

    private float horizontalInput;
    private float verticalInput;
    //private float r_horizontalInput;
    //private float r_verticalInput;

    public Game_Controller gameController;
    public int pickUpCount = 0;
    public int countTreshold = 3;

    public Joystick joystick;

    AudioManager audioManager;


    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        tresholdRotation = Quaternion.Euler(0, 100, 0);
        nextFire = Time.time;

        if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal") + joystick.Horizontal;
        verticalInput = Input.GetAxis("Vertical") + joystick.Vertical;
        
        //r_horizontalInput = Input.GetAxis("R_Horizontal");
        //r_verticalInput = Input.GetAxis("R_Vertical");

        // Win condition
        Debug.Log(gameController.play);
        if (pickUpCount >= gameController.winCondition && gameController.play)
        {
            gameController.WinLevel();
            activeControls = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activeControls)
        {
            transform.position += transform.forward * moveSpeed * Time.fixedDeltaTime; // Go forward at all times.

            if (Mathf.Abs(verticalInput) > 0.25 || Mathf.Abs(horizontalInput) > 0.25)
            {
                Movement();
            }
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

    void OnTriggerEnter(Collider other)
    {
        // Snack
        if (other.gameObject.CompareTag("Snack"))
        {
            BodyPartObject.GetComponent<Player_GrowScript>().Grow();
            other.gameObject.SetActive(false);
            audioManager.Play("EatSound");
            pickUpCount++;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            gameController.GameOver();
            activeControls = false;
        }

        if (other.gameObject.CompareTag("Collider"))
        {
            gameController.GameOver();
            activeControls = false;
        }
    }
    
    #region Old broken Shooting Mechanics
    /*
        //if (Mathf.Abs(r_horizontalInput) > 0.25 || Mathf.Abs(r_verticalInput) > 0.25)
       //{
       //    Shoot();
       //}
     
    void Shoot()
    {
        Vector3 Xshoot = right * moveSpeed * Time.deltaTime * r_horizontalInput;
        Vector3 YShoot = forward * moveSpeed * Time.deltaTime * r_verticalInput;
        Vector3 heading = new Vector3((Xshoot.x + YShoot.x), 0, (Xshoot.z + YShoot.z));



        if (heading == Vector3.zero)
        {
            return;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(heading, Vector3.up);
            TurretObject.transform.rotation = rotation;

            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Transform bulletTransform = Instantiate(bullet, gunEndPoint.position, Quaternion.identity);

                bulletTransform.GetComponent<Bullet>().Setup(heading);
            }

        }
    }
    */
    #endregion
}