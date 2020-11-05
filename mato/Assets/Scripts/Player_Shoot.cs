using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    public float bulletForce = 100f;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Shoot();

        }
    }

    void Shoot()
    {
        Transform bulletTransform = Instantiate(bullet, transform.position, Quaternion.identity);

        Vector3 shootDirection = transform.forward.normalized;
        bulletTransform.GetComponent<Bullet>().Setup(shootDirection);
    }
}
