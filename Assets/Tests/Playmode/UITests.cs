using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UITests
{


  
    [UnityTest]
    public IEnumerator MeshParameterUpdate()
    {
        var gameObject = new GameObject();
        var generator = gameObject.AddComponent<GenerateMesh>();

        generator.UpdateParameters(10,10,10,0.05f);

        Assert.AreEqual(10,generator.xSize);
        Assert.AreEqual(10,generator.zSize);
        Assert.AreEqual(10,generator.height);
        Assert.AreEqual(0.05f,generator.xNoise);


        yield return null;
    }

      
    [UnityTest]
    public IEnumerator VillageToggle()
    {
        var gameObject = new GameObject();
        var toggle = gameObject.AddComponent<GenerateBiomes>();

        var previousState = toggle.toggleCities;
        toggle.setToggleCities(true);
        
        Assert.AreNotEqual(previousState,toggle.toggleCities);

        yield return null;
    }
    [UnityTest]
        public IEnumerator RockToggle()
    {
        var gameObject = new GameObject();
        var toggle = gameObject.AddComponent<GenerateBiomes>();

        var previousState = toggle.toggleRocks;
        toggle.setToggleRocks(true);
        
        Assert.AreNotEqual(previousState,toggle.toggleRocks);

        yield return null;
    }
        [UnityTest]
        public IEnumerator PlainsToggle()
    {
        var gameObject = new GameObject();
        var toggle = gameObject.AddComponent<GenerateBiomes>();

        var previousState = toggle.togglePlains;
        toggle.setTogglePlains(true);
        
        Assert.AreNotEqual(previousState,toggle.togglePlains);

        yield return null;
    }
        [UnityTest]
        public IEnumerator ForestToggle()
    {
        var gameObject = new GameObject();
        var toggle = gameObject.AddComponent<GenerateBiomes>();

        var previousState = toggle.toggleForest;
        toggle.setToggleForest(true);
        
        Assert.AreNotEqual(previousState,toggle.toggleForest);

        yield return null;
    }
}
