using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class Tiles : MonoBehaviour {

	public List<Tile> tiles;
	public Tile tile;
	public Transform container;

	int tilesWidth = 30;
	int tilesWHeight = 20;
	float[,] tilesmap;
	public List<Point> path;
	Grid grid;

	void Start () {
		
		tilesmap = new float[tilesWidth, tilesWHeight];

		int id = 0;
		for (int a = 0; a < tilesWidth; a++) {
			for (int b = 0; b < tilesWHeight; b++) {

				bool isWalkable = false;
				if (Random.Range (0, 10) < 9) {
					tilesmap [a, b] = 1;
					isWalkable = true;
				} else {
					Events.AddGenericObject (new Vector2(a,b));
				}
				Tile newTile = Instantiate (tile);
				newTile.transform.SetParent (container);
				newTile.Init(isWalkable, new Vector3 (a, 0, b));
				tiles.Add (newTile);
			}
		}		
		grid= new Grid(tilesWidth, tilesWHeight, tilesmap);
	}
	public List<Point> GetPathfinder(Vector3 _from , Vector3 _to )
	{		
		Point _f = new Point ((int)_from.x, (int)_from.z); 
		Point _t = new Point ((int)_to.x, (int)_to.z);
		//print(_f.x + " - " + _f.y + "     -      " + _t .x + " - " + _t .y);
		List<Point> list = Pathfinding.FindPath(grid, _f, _t);
		foreach (Tile tile in tiles) {
			bool isOn = false;
			foreach (Point p in list) {
				//print ((int)tile.transform.localPosition.x + " - " + (int)tile.transform.localPosition.y);
				if (p.x == (int)tile.transform.localPosition.x && p.y == (int)tile.transform.localPosition.z) {
					isOn = true;
					tile.MarkAsPath();
				}
			}
			if(!isOn)
				tile.ResetPath();
		}
		
		return list;

	}
	public Vector3 GetFreeTileInCenter()
	{
		for (int a = tilesWidth/3; a < tilesWidth; a++) {
			for (int b = 0; b < tilesWHeight/3; b++) {
				if (tilesmap [a, b] == 0)
					return new Vector3 (a, 0, b);
			}
		}
		return Vector3.one;
	}

}
