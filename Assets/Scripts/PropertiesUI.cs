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
    float generatedScene = 0f;
    bool createBiomes = false;
    void Update()
    {
        if(timer < 0.15f){
            timer +=1*Time.deltaTime;
        }else{
            timer = 0f;
   

         water.UpdateWaterLevel(waterLevelSlider.value);
        }

         if(createBiomes == true){
             if(generatedScene > 0.2f)
             {

            createBiomes = false;
            generatedScene = 0f;
            onBiomesGenerateClick();
             }else
             {
            generatedScene +=1*Time.deltaTime;
             }
        
        }
          if (Input.GetKeyDown(KeyCode.Escape))
        {
            onRestartClick();
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
    public void onRandomizeTerrainClick()
    {
         mesh.xNoise = Random.Range(xNoiseSlider.minValue+0.05f,xNoiseSlider.maxValue-0.05f);
         mesh.zNoise = Random.Range(zNoiseSlider.minValue+0.05f,zNoiseSlider.maxValue-0.05f);
         mesh.height = Random.Range(heightSlider.minValue,heightSlider.maxValue*0.8f);
         mesh.roughness = Random.Range(terrainRoughness.minValue,terrainRoughness.maxValue-0.03f);
         int mapSize = Random.Range(150,250);
        seed.text = Random.Range(0,999999).ToString();
        gameSeed.SetSeed(seed.text.GetHashCode());
        biomes.clearObjects();

      
        xNoiseSlider.value = mesh.xNoise;
        
        zNoiseSlider.value =mesh.zNoise;
        
        heightSlider.value =mesh.height;
        
        mesh.xSize = mapSize;
        mesh.zSize = mapSize;
        mapSizeSlider.value =mapSize;
        
        terrainRoughness.value = mesh.roughness;
        mesh.GenerateMap();
    
    }
    public void GenerateRandomBiome()
    {
            forestDensity.value = Random.Range(forestDensity.minValue,forestDensity.maxValue);
            foliageDensity.value = Random.Range(foliageDensity.minValue,foliageDensity.maxValue);
            RockDensity.value = Random.Range(RockDensity.minValue,RockDensity.maxValue);
            buildingQuantity.value = Random.Range(buildingQuantity.minValue,buildingQuantity.maxValue);
            buildingSpacing.value = Random.Range(buildingSpacing.minValue,buildingSpacing.maxValue);
      
        
    }
    public void onRandomizeEverythingClick()
    {
        onRandomizeTerrainClick();
        GenerateRandomBiome();
        createBiomes = true;
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

