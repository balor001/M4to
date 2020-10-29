using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GrowScript : MonoBehaviour
{
    private int matoBodySize = 0;
    private List<GameObject> matoBodyParts;
    private float zPos;
    public GameObject bodyPart;

    // Start is called before the first frame update
    void Start()
    {
        matoBodyParts = new List<GameObject>();
        matoBodyParts.Capacity = matoBodySize;

    }

    void Grow()
    {
        matoBodyParts.Add(bodyPart);

        for (int i = 0; i < matoBodySize; i++)
        {

            zPos = zPos - 1.25f;
            Debug.Log(zPos);

            GameObject bPart = Instantiate(bodyPart);
            bPart.transform.parent = transform;
            bPart.transform.localPosition = new Vector3(0, 0, zPos);
            bPart.transform.rotation = new Quaternion(0, 0, 0, 0);
            bPart.name = "BodyPart_" + matoBodySize; 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snack"))
        {
            matoBodySize++;
            Grow();
            Destroy(other.gameObject);
        }
    }
}
