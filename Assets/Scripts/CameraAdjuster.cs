using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraAdjuster : MonoBehaviour
{
    public Slider mapSizeSlider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(mapSizeSlider.value/2,transform.position.y,transform.position.z);
    }
}
