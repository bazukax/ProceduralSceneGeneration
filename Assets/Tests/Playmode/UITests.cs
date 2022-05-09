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
    [UnityTest]
            public IEnumerator WaterLevelSlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator MapSizeSlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator MapHeightSlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator ZNoiseSlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator XNoiseSLider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator Roughness()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator ForestDensitySlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator FoliageDensitySlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator RockDensitySlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator BuildingQuantitySlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator BuildingSpacingSlider()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator GenerateTerrainButton()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator GenerateBiomeButton()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator ExportButton()
    {
     

        yield return null;
    }
      [UnityTest]
            public IEnumerator RandomizeButton()
    {
     

        yield return null;
    }
}
