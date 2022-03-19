using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateBiomes : MonoBehaviour
{

    public bool toggleForest = true;
    public bool togglePlains = true;
    public bool toggleRocks= true;
    public bool toggleCities = true;

    public GameObject[] trees;
    public GameObject[] rocks;
    public GameObject[] plains;
    public GameObject[] buildings;

    public List<GameObject> mapObjects;

    float forestDesnity = 950;
    float plainsDensity = 950;
    float buildingSpacing = 3;
    float buildingQuantity = 950;
    float rockDensity = 980;
    int[,] biomeMap;
    float waterLevel = 0;


    public void setToggleRocks(bool state)
    {   
        toggleRocks = !toggleRocks;
    }
     public void setTogglePlains(bool state)
    {   
        togglePlains = !togglePlains;
    }
     public void setToggleForest(bool state)
    {   
        toggleForest = !toggleForest;
    }
     public void setToggleCities(bool state)
    {   
        toggleCities = !toggleCities;
    }

    public void setWaterLevel(float height)
    {
        waterLevel = height+3;
    }

    public void setForestDesnity(float density)
    {
        forestDesnity = density;
    }
        public void setPlainDensity(float density)
    {
        plainsDensity = density;
    }
        public void setRockDensity(float density)
    {
        rockDensity = density;
    }
        public void setBuildingDensity(float density)
    {
        buildingQuantity = density;
    }
        public void setBuildingSpacing(int spacing)
    {
        buildingSpacing = spacing;
    }

    public void clearObjects()
    {
              foreach(GameObject entity in mapObjects)
                {
                    Destroy(entity.gameObject);
                }
    }
    public void generateBiomes(int xSize = 0, int zSize=0)
    {
                 

        biomeMap = new int[300,300];

        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                biomeMap[x,z] = 0; //plains
            }
        }

        int perlinStartingPoint = Random.Range(0, 5000000);
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float y = 0;
                y = Mathf.PerlinNoise((x + perlinStartingPoint) * 0.02f, (z + perlinStartingPoint) * 0.02f);
                if (y >= 0.5f)
                {
                    biomeMap[x, z] = 1; //woods
                }
            }
        }

        perlinStartingPoint = Random.Range(0, 5000000);
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float y = 0;
                y = Mathf.PerlinNoise((x + perlinStartingPoint) * 0.02f, (z + perlinStartingPoint) * 0.02f);
                if (y >= 0.7f)
                {
                    if (biomeMap[x, z] == 0)
                    {
                        biomeMap[x, z] = 2; //rocks
                    }
                }
            }
        }
         perlinStartingPoint = Random.Range(0, 5000000);
        for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float y = 0;
                y = Mathf.PerlinNoise((x + perlinStartingPoint) * 0.02f, (z + perlinStartingPoint) * 0.02f);
                if (y >= 0.5f)
                {
                    if (biomeMap[x, z] == 0)
                    {
                        biomeMap[x, z] = 3; //cities
                    }
                }
            }
        }


    }


    public void generateFoliage(int xSize, int zSize)
    {

      for (int z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

             


                if(biomeMap[x,z]==0 )spawnPlainsEntity(x,z);
                if(biomeMap[x,z]==1 )spawnForrestEntity(x,z);
                if(biomeMap[x,z]==2 )spawnRocksEntity(x,z);
                if(x>0 && z>0){
                 if(biomeMap[x,z]==3 && x%buildingSpacing==1 && z%buildingSpacing==1)spawnCityEntity(x,z);
                }

            }
        }

    }
    public void spawnPlainsEntity(int posX,int posZ)
    {
         int chance = Random.Range(0,1000);
         if(togglePlains == false)return;
        if(chance>plainsDensity)return;
          Vector3 pos = new Vector3(posX,200,posZ);
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
        GameObject gameObject = plains[Random.Range(0,plains.Length)];
        GameObject ground = Instantiate(gameObject, hit.point,gameObject.transform.rotation);
        mapObjects.Add(ground);
        ground.transform.rotation = Quaternion.Euler(0, Random.Range(0,360),0);
        if(ground.transform.position.y <waterLevel || ground.transform.position.y > 25 )Destroy(ground.gameObject);
        }
    }
    public void spawnForrestEntity(int posX,int posZ)
    {
        int chance = Random.Range(0,1000);
        if(toggleForest == false)return;
        if(chance>forestDesnity)return;
          Vector3 pos = new Vector3(posX,200,posZ);
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
        GameObject gameObject = trees[Random.Range(0,trees.Length)];
        GameObject ground = Instantiate(gameObject, hit.point,gameObject.transform.rotation);
        mapObjects.Add(ground);
        ground.transform.rotation = Quaternion.Euler(0, Random.Range(0,360),0);
        if(ground.transform.position.y < waterLevel)Destroy(ground.gameObject);
        }
    }
    public void spawnRocksEntity(int posX,int posZ)
    {
         int chance = Random.Range(0,1000);
         if(toggleRocks == false)return;
        if(chance>rockDensity)return;
          Vector3 pos = new Vector3(posX,200,posZ);
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
        GameObject gameObject = rocks[Random.Range(0,rocks.Length)];
        GameObject ground = Instantiate(gameObject, hit.point,gameObject.transform.rotation);
        mapObjects.Add(ground);
        ground.transform.rotation = Quaternion.Euler(0, Random.Range(0,360),0);
        if(ground.transform.position.y < waterLevel)Destroy(ground.gameObject);
        }

    }
       public void spawnCityEntity(int posX,int posZ)
    {
         int chance = Random.Range(0,1000);
         if(toggleCities == false)return;
        if(chance>buildingQuantity)return;
          Vector3 pos = new Vector3(posX,200,posZ);
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
        GameObject gameObject = buildings[Random.Range(0,buildings.Length)];
        GameObject ground = Instantiate(gameObject, hit.point,gameObject.transform.rotation);
        mapObjects.Add(ground);
        ground.transform.rotation = Quaternion.Euler(0, Random.Range(0,360),0);
        if(ground.transform.position.y < waterLevel)Destroy(ground.gameObject);
        }

    }

}
