using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFoliage : MonoBehaviour
{
    public GameObject[] rocks;
    public List<GameObject> worldObjects;
    public void generateFoliage()
    {
    for(int i = 0; i < 300; i++)
    {
        Vector3 pos = new Vector3(Random.Range(0,150),100,Random.Range(0,150));
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
        GameObject selected = rocks[Random.Range(0,rocks.Length)];
        GameObject ground = Instantiate(selected, hit.point,selected.transform.rotation);
        ground.transform.rotation = Quaternion.Euler(0, Random.Range(0,360),0);
        }
    }

    }
}
