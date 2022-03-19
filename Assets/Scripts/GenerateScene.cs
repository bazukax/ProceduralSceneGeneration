using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateScene : MonoBehaviour
{
   
    public static int mapX=100;
    public static int mapZ=100;

    public float[,] mapArray = new float[mapX,mapZ];

    public GameObject cube;
    public GameObject grass;

    void Start()
    {
        for(int x = 0; x<mapX;x++)
        {
            for(int z = 0; z<mapX;z++)
            {
                    mapArray[x,z] = Mathf.PerlinNoise(x*0.003f,z*0.003f) *1;
                   // Instantiate(cube, new Vector3(x, mapArray[x,z], z), Quaternion.identity);
                    Instantiate(grass, new Vector3(x, mapArray[x,z]+1, z), Quaternion.identity);
                    
            }
        }

       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
