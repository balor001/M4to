using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GrowScript : MonoBehaviour
{
    private int matoBodySize = 0;
    private List<GameObject> matoBodyParts;
    private float zPos;
    private float scaleMultiplier = 1;

    public GameObject bodyPart;
    public GameObject tailPart;

    // Start is called before the first frame update
    void Start()
    {
        matoBodyParts = new List<GameObject>();
        matoBodyParts.Capacity = matoBodySize;

    }

    void Grow()
    {
        matoBodyParts.Add(bodyPart);

        zPos = zPos - 1.25f;
        //scaleMultiplier = scaleMultiplier * 0.8f;


        Debug.Log(zPos);

        GameObject bPart = Instantiate(bodyPart);
        bPart.transform.parent = transform;
        bPart.transform.localPosition = new Vector3(0, 0, zPos);
        bPart.transform.rotation = new Quaternion(0, 0, 0, 0);
        bPart.transform.localScale = new Vector3(scaleMultiplier, scaleMultiplier, 1);
        bPart.name = "BodyPart_" + matoBodySize;

        tailPart.transform.parent = transform;
        tailPart.transform.localPosition = new Vector3(0, 0, zPos - 1.5f);
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
