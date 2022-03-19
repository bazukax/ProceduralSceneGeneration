using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesUI : MonoBehaviour
{
    public InputField seed;
    public Slider xNoiseSlider;
    public Slider zNoiseSlider;
    public Slider mapSizeSlider;

    public Slider heightSlider;
    public Slider waterLevelSlider;
    public Slider terrainRoughness;

    public Slider forestDensity;
    public Slider foliageDensity;
    public Slider RockDensity;
    public Slider buildingQuantity;
    public Slider buildingSpacing;
    public GenerateMesh mesh;

    public GenerateBiomes biomes;
    public WaterController water;
    public Seed gameSeed;

    float timer =0f;
    void Update()
    {
        if(timer < 0.15f){
            timer +=1*Time.deltaTime;
        }else{
            timer = 0f;
   

         water.UpdateWaterLevel(waterLevelSlider.value);
        }

    }
    public void onGenerateClick()
    {
        gameSeed.SetSeed(seed.text.GetHashCode());
        biomes.clearObjects();

        mesh.xNoise = xNoiseSlider.value;
        mesh.zNoise = zNoiseSlider.value;
        mesh.height = heightSlider.value;
        mesh.xSize = (int)mapSizeSlider.value;
        mesh.zSize = (int)mapSizeSlider.value;
        mesh.roughness = terrainRoughness.value;
        mesh.GenerateMap();
    
       


    }
    public void onBiomesGenerateClick()
    {
         gameSeed.SetSeed(seed.text.GetHashCode());
        biomes.clearObjects();
        biomes.setForestDesnity(forestDensity.value);
        biomes.setPlainDensity(foliageDensity.value);
        biomes.setRockDensity(RockDensity.value);
        biomes.setBuildingDensity(buildingQuantity.value);
        biomes.setBuildingSpacing((int)buildingSpacing.value);
        biomes.setWaterLevel(waterLevelSlider.value);

        gameSeed.SetSeed(seed.text.GetHashCode());
        biomes.generateBiomes((int)mapSizeSlider.value,(int)mapSizeSlider.value); //todo times scale
        biomes.generateFoliage((int)mapSizeSlider.value,(int)mapSizeSlider.value);//todo times scale
        water.UpdateWaterLevel(waterLevelSlider.value);
    }
    public void onRestartClick()
    {
        Application.LoadLevel(0);
    }
     public void onSetSeedClick()
    {
        gameSeed.SetSeed(seed.text.GetHashCode());
    }
}

