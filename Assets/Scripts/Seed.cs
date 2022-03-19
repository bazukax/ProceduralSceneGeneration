using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public string gameSeed = "Seed";
    public int currentSeed = 0;
    
    private void Awake()
    {
        currentSeed = gameSeed.GetHashCode();
     //   Random.InitState(currentSeed);
    }

    public void SetSeed(int seed)
    {
        Random.InitState(seed);
    }

}
