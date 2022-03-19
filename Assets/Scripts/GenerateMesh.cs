using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GenerateMesh : MonoBehaviour
{
    Mesh mesh;
    Color[] colors;
    Vector3[] vertices;
    int[] triangles;
    public GenerateFoliage foliageGen;
    public int xSize = 20;
    public int zSize = 20;
    public float height = 5;
    public float xNoise = 0.06f;
    public float zNoise = 0.06f;
    public float roughness = 1.01f;

    float[,] publicFalloffMap;
    public Gradient gradient;
    MeshFilter meshFilter;
    MeshCollider meshCollider;

    float minTerrainHeight;
    float maxTerrainHeight;
    public void Start()
    {
        meshFilter =  GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
    }
    public void UpdateParameters(int _xSize = 0, int _zSize = 0, float _height = 5, float _xNoise = 0.05f)
    {
        xSize = _xSize;
        zSize = _zSize;
        height = _height;
        xNoise = _xNoise;
    }
    public void GenerateMap()
    {
        publicFalloffMap = GenerateFalloff(new Vector2Int(xSize+1, zSize+1), 0.2f, 1f);
        mesh = new Mesh();
        meshFilter.mesh = mesh;
        CreateShape();
        UpdateMesh();

    }
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        int randomStartPoint = Random.RandomRange(1, 60000);
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
              
               //float y = Mathf.PerlinNoise((x + randomStartPoint) * xNoise/20, (z + randomStartPoint) * zNoise/20);
               float y = 0;
               
                //y = Mathf.PerlinNoise((x + randomStartPoint) * xNoise, (z + randomStartPoint) * zNoise) * height * Random.RandomRange(1f, roughness);
               //  y = 2*(0.5f-Mathf.Abs((0.5f- Mathf.PerlinNoise((x + randomStartPoint) * xNoise *0.2f, (z + randomStartPoint) * zNoise  *0.2f) * height * Random.RandomRange(1f, roughness))));
                  y +=  Mathf.PerlinNoise((x + randomStartPoint) * xNoise *0.8f, (z + randomStartPoint) * zNoise  *0.8f) * height * Random.RandomRange(1f, roughness);
                y +=  Mathf.PerlinNoise((x + randomStartPoint) * xNoise *0.5f, (z + randomStartPoint) * zNoise  *0.5f) * height * Random.RandomRange(1f, roughness);
                y +=  Mathf.PerlinNoise((x + randomStartPoint) * xNoise *0.2f, (z + randomStartPoint) * zNoise  *0.2f) * height * Random.RandomRange(1f, roughness);
                //y *= 2*(0.5 - Mathf.Abs(0.5f - Mathf.PerlinNoise(x,y)));
            
              
                y = y * publicFalloffMap[x, z];

                vertices[i] = new Vector3(x, y, z);

                if(y>maxTerrainHeight) maxTerrainHeight = y;
                if(y < minTerrainHeight)minTerrainHeight=y;

                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {

                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
        colors = new Color[vertices.Length];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight,maxTerrainHeight, vertices[i].y);
              colors[i] = gradient.Evaluate(height);
                i++;
            }
        }

    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;
        mesh.RecalculateNormals();
        if (this.gameObject.GetComponent<MeshCollider>() == null)
        {
            this.gameObject.AddComponent<MeshCollider>();
        }
        else
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
            this.gameObject.AddComponent<MeshCollider>();
        }

        //transform.position = new Vector3(-xSize/2,0,-zSize/2);
    }

    public static float[,] GenerateFalloff(Vector2Int size, float fallofffStart, float falloffEnd)
    {
        float[,] heightMap = new float[size.x, size.y];
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector2 position = new Vector2((float)x / size.x * 2 - 1, (float)y / size.y * 2 - 1);

                float t = Mathf.Max(Mathf.Abs(position.x), Mathf.Abs(position.y)); // finding value closer to the edge

                if (t < fallofffStart)
                {
                    heightMap[x, y] = 1;
                }
                else if (t > falloffEnd)
                {
                    heightMap[x, y] = 0;
                }
                else
                {
                    heightMap[x, y] = Mathf.SmoothStep(1, 0, Mathf.InverseLerp(fallofffStart, falloffEnd, t));
                }
            }

        }
        return heightMap;
    }

}
