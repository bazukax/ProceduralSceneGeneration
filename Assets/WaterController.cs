using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    
    public void UpdateWaterLevel(float height)
    {
        transform.position = new Vector3(transform.position.x,height,transform.position.z);
    }
}
