using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BiomeGenerationTests
{


  
    [UnityTest]
    public IEnumerator ClearObject()
    {

        var gameObject = new GameObject();
        var emptyObject = new GameObject();
        gameObject.AddComponent<GenerateBiomes>();

        GenerateBiomes gb = gameObject.GetComponent<GenerateBiomes>();
        gb.mapObjects = new List<GameObject>();
       gb.mapObjects.Add(emptyObject);
       gb.mapObjects.Add(emptyObject);
       gb.mapObjects.Add(emptyObject);
       gb.mapObjects.Add(emptyObject);
       gb.clearObjects();

        Assert.AreEqual(0,gameObject.GetComponent<GenerateBiomes>().mapObjects.Count);

        yield return null;
    }

      
    [UnityTest]
    public IEnumerator VillageBiome()
    {
        var gameObject = new GameObject();

        yield return null;
    }
    [UnityTest]
        public IEnumerator RockBiome()
    {
        var gameObject = new GameObject();

        yield return null;
    }
        [UnityTest]
        public IEnumerator PlainsBiome()
    {
   

        yield return null;
    }
            [UnityTest]
        public IEnumerator ForrestBiome()
    {
        var gameObject = new GameObject();


        yield return null;
    }
        [UnityTest]
        public IEnumerator SpawnEntity()
    {
        var gameObject = new GameObject();


        yield return null;
    }
            [UnityTest]
        public IEnumerator ObjectRangeBound()
    {
        var gameObject = new GameObject();


        yield return null;
    }
}
