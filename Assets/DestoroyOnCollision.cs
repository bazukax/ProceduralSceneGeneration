using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoroyOnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     //   Destroy(this.gameObject.GetComponent<DestoroyOnCollision>(),10f);
    }

    

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("entered col");
        if(collision.gameObject.tag == "House")
        {
            Destroy(this.gameObject);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("entered col");
        if(collision.gameObject.tag == "House")
        {
            Destroy(this.gameObject);
        }
    }
}
