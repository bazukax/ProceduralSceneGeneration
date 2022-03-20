using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomata : MonoBehaviour
{
  
    public static int width=70;
    public static int height=70;

	public static int iterations = 5;

    public int[,] mapArray = new int[width,height];

    public GameObject[] tiles;
	List<List<Point>> caverns = new List<List<Point>>(); // list of found caverns	
  

    void Start()
    {
        //generate random array
        for(int x = 0; x<width;x++)
        {
            for(int z = 0; z<height;z++)
            {
                    mapArray[x,z] = Random.Range(0,2);
                   // Instantiate(cube, new Vector3(x, mapArray[x,z], z), Quaternion.identity);

                  //  Instantiate(floor, new Vector3(x, mapArray[x,z]+1, z), Quaternion.identity);
                    
            }
        }
		for(int i = 0; i < iterations; i++)
		{
            applyRules();
		}
		addMatrixBorders(mapArray);
		//findAllCaverns();
		//cullSmallerCaverns();
                
          for(int x = 0; x<width;x++)
        {
            for(int z = 0; z<height;z++)
            {
                
                   Instantiate(tiles[mapArray[x,z]], new Vector3(x, 0, z), Quaternion.identity);

                    
            }
        }
    }
    public void applyRules() {
	// determine how many wall neighbors surround each cell (IMPORTANT: do this to all cells before applying rules to any of them)
	int[,] numWallNeighbors = new int[width,height]; //numWallNeighbors[x][y] = number of wall neighbors of a cell at (x, y)
	for(int x = 0; x < width; x++){
		for(int y = 0; y < height; y++){
			numWallNeighbors[x,y] = GetNeighbours(x,y);
		}
	}
	// now apply the rules to each cell
	for(int x = 0; x < width; x++) {
		for(int y = 0; y < height; y++) {
			int currentState = mapArray[x,y]; // a cell's current state
			int newState; // a cell's state will change to this
			int numNeighbors = numWallNeighbors[x,y]; // how many neighbors a cell has
			// if a wall has less than 4 wall neighbors, then it becomes a passage.
			if(currentState == 1 && numNeighbors < 4) {
				newState = 0;
			}
			// if a passage has 5 or more wall neighbors, then it becomes a wall
			else if(currentState == 0 && numNeighbors >= 5) {
				newState = 1;
			}
			// otherwise its state remains the same
			else {
				newState = currentState;
			}
		
			mapArray[x,y] = newState;
		}
	}
}
    int GetNeighbours(int x,int y)
    {
	int numWallNeighbors = 0;
	// determine if the following neighbors are walls, increase numWallNeighbors accordingly:
	// neighbor 1 @ (x - 1, y - 1)
	if((x - 1 >= 0 && y - 1 >= 0 && mapArray[x - 1,y - 1] == 1) || (x - 1 < 0 && y - 1 < 0)) {
		numWallNeighbors += 1;
	}
	// neighbor 2 @ (x, y - 1)
	if((y - 1 >= 0 && mapArray[x,y - 1] == 1) || (y - 1 < 0)) {
		numWallNeighbors += 1;
	}
	// neighbor 3 @ (x + 1, y - 1)
	if((x + 1 < width && y - 1 >= 0 && mapArray[x + 1,y - 1] == 1) || (x + 1 >= width && y - 1 < 0)) {
		numWallNeighbors += 1;
	}
	// neighbor 4 @ (x - 1, y)
	if((x - 1 >= 0 && mapArray[x - 1,y] == 1) || (x - 1 < 0)) {
			numWallNeighbors += 1;
	}
	// neighbor 5 @ (x + 1, y)
	if((x + 1 < width && mapArray[x + 1,y] == 1) || (x + 1 >= width)) {
		numWallNeighbors += 1;
	}
	// neighbor 6 @ (x - 1, y + 1)
	if((x - 1 >= 0 && y + 1 < height && mapArray[x - 1,y + 1] == 1) || (x - 1 < 0 && y + 1 >= height)) {
		numWallNeighbors += 1;
	}
	// neighbor 7 @ (y + 1)
	if((y + 1 < height && mapArray[x,y + 1] == 1) || (y + 1 >= height)) {
		numWallNeighbors += 1;
	}
	// neighbor 8 @ (x + 1, y + 1)
	if((x + 1 < width && y + 1 < height && mapArray[x + 1,y + 1] == 1) || (x + 1 >= width && y + 1 >= height)) {
		numWallNeighbors += 1;
	}
	return numWallNeighbors;
    }
	public void addMatrixBorders(int[,] cavernMap)
	{
		for(int i = 0; i < width;i++)
		{
			cavernMap[i,0] = 1; //bottom
			cavernMap[i,height-1] = 1; // top
			cavernMap[0,i] = 1; //left
			cavernMap[width-1,i] = 1; //right
		}
		
	}

	public class Point
	{
		public Point(int X = 0,int Y = 0)
		{
			x = X;
			y=Y;
		}
	
		public int x,y;
	}
	public List<Point> findCavernAt(Point initialPoint)
	{
		// Flood Fill Algorithm
	// 1. create 2 empty lists (toBeFlooded: a list of points about to be flooded; flooded: points that have been flooded)
	// 2. add initialPoint to toBeFlooded list
	// 3. get the point at index 0 in toBeFlooded list
	// 4. find the 4 non-flooded points surrounding point (point.y - 1, point.x - 1, point.x + 1, point.y + 1) and add them to toBeFlooded list (if they are non-flooded passageways)
	// 5. add point to flooded list
	// 6. remove point from toBeFlooded list
	// 7. repeat step 3

	List<Point> toBeFlooded = new List<Point>();
	List<Point> flooded = new List<Point>();
	toBeFlooded.Add(initialPoint);
	while(toBeFlooded.Count >= 0 &&toBeFlooded != null){
		int x = toBeFlooded[0].x;
		int y = toBeFlooded[0].y;
	

		//if(!toBeFlooded.Contains(new Point(x,y+1)) && !flooded.Contains(new Point(x,y+1)) && mapArray[x,y - 1] == 0)	
		if(flooded.Contains(new Point(x,y)))
		{
			continue;
		}
		if(toBeFlooded.Contains(new Point(x,y)))
		{
			continue;
		}
		if(!flooded.Contains(new Point(x, y - 1)) && !toBeFlooded.Contains(new Point(x, y - 1)) && mapArray[x,y - 1] == 0) {
			toBeFlooded.Add(new Point(x, y - 1));

		}
		if(!flooded.Contains(new Point(x - 1, y)) && !toBeFlooded.Contains(new Point(x - 1, y))&& mapArray[x - 1,y] == 0) {
			toBeFlooded.Add(new Point(x - 1, y));
		}
		if(!flooded.Contains(new Point(x + 1, y)) && !toBeFlooded.Contains(new Point(x + 1, y)) && mapArray[x + 1,y] == 0) {
			toBeFlooded.Add(new Point(x + 1, y));
		}
		if(!flooded.Contains(new Point(x, y + 1)) && !toBeFlooded.Contains(new Point(x, y + 1)) && mapArray[x,y + 1] == 0) {
			toBeFlooded.Add(new Point(x, y + 1));
		}
		flooded.Add(new Point(toBeFlooded[0].x, toBeFlooded[0].y));
		toBeFlooded.RemoveAt(0);
	}
	// if there is no cavern at initialPoint, then set flooded to null, as no cave exists here
	if(flooded.Capacity == 0) {
		flooded = null;
	}
	return flooded;
	}



//finds all caverns, returns them as a list of caverns (size 0 if there are no caverns), each represented by a list of of points
public List<List<Point>> findAllCaverns() {
	List<Point> flooded = new List<Point>(); // list of points already flooded by the flood fill algorithm (points in found caverns)
	caverns = new List<List<Point>>(); // list of found caverns
	// loop through every point in the cell grid
	for(int x = 0; x < width; x++) {
		for(int y = 0; y < height; y++) {
			// if cell at (x, y) is a passage, and it has not already been flooded (i.e. is not in a cavern that's already been found) then find the cavern it's connected to
			if(mapArray[x,y] == 0 && !flooded.Contains(new Point(x, y))) {
				// find cavern at (x, y) 
				List<Point> cavern = findCavernAt(new Point(x, y));
				// if there is a cavern at (x, y), then update flooded list and add to caverns list
				if(cavern != null) {
					flooded.AddRange(cavern);///////////////////////////////may neeed fixing
					caverns.Add(cavern);
				}
			}
		}
	}
	return caverns;
}
public List<Point> findBiggestCavern(List<List<Point>> caverns)
{
	int biggestCavernId = 0;
	int biggestCavernCapacity = 0;
	int totalCaverns = caverns.Capacity;

	for(int i = 0; i < totalCaverns; i++)
	{
		if(caverns[i].Capacity > biggestCavernCapacity)
		{
			biggestCavernId = i;
		}
	} 
	return caverns[biggestCavernId];

}
public void cullSmallerCaverns() {
	List<Point> biggestCavern = findBiggestCavern(caverns);
	if(biggestCavern != null) {
		
		// fill in cell grid with walls
		for(int x = 0; x < width; x++){
			for(int y = 0; y < height; y++){
				mapArray[x,y] = 1;
			}
		}
		// fill in points of biggest cavern with passageways
		foreach(Point point in biggestCavern)
		{
			int x = point.x;
			int y = point.y;
			mapArray[x,y] = 0;
		}
		// now only the biggest cavern will remain in the cell grid
	}
}


    void Update()
    {
        
    }
}
