using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class Tiles : MonoBehaviour {

	public List<Tile> tiles;
	public Tile tile;
	public SceneIngame sceneInGame;
	int tilesWidth = 14;
	int tilesWHeight = 8;
	float[,] tilesmap;
	public List<Point> path;
	NesScripts.Controls.PathFind.Grid grid;

	void Start () {
		Events.Blocktile += Blocktile;
		tilesmap = new float[tilesWidth, tilesWHeight];
		string[] randomObjects = new string[3];
		randomObjects [0] = "florero";
		randomObjects [1] = "arbol";
		randomObjects [2] = "sillon";
		int id = 0;
		for (int a = 0; a < tilesWidth; a++) {
			for (int b = 0; b < tilesWHeight; b++) {

				bool isWalkable = false;
				if (Random.Range (0, 10) < 9) {
					tilesmap [a, b] = 1;
					isWalkable = true;
				} else {
					SceneObjectData data = new SceneObjectData ();
					data.sceneObjectName = randomObjects [Random.Range (0, randomObjects.Length-1)];
					Events.AddGenericObject (data, new Vector2(a,b));
				}
				Tile newTile = Instantiate (tile);
				newTile.transform.SetParent (sceneInGame.tilesContainer);
				newTile.Init(isWalkable, new Vector3 (a, 0, b));
				tiles.Add (newTile);
			}
		}		
		grid= new NesScripts.Controls.PathFind.Grid(tilesWidth, tilesWHeight, tilesmap);
	}
	void Blocktile(Tile tile, bool isBlock)
	{
		int value = 0;

		if (!isBlock) {
			tile.SetAsWalkable ();
			value = 1;
		} else {
			tile.SetAsUnwalkable ();
			value = 0;
		}
		//print ((int)tile.transform.localPosition.x + " __ " + (int)tile.transform.localPosition.z + "Blocktile ____" + isBlock + "  :  " + value);
		tilesmap [(int)tile.transform.localPosition.x, (int)tile.transform.localPosition.z] = value;
		grid= new NesScripts.Controls.PathFind.Grid(tilesWidth, tilesWHeight, tilesmap);

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
		for (int a = (int)(tilesWidth/2f); a < tilesWidth; a++) {
			for (int b = (int)(tilesWHeight/2); b < tilesWHeight; b++) {
				if (tilesmap [a, b] == 1)
					return new Vector3 (a, 0, b);
			}
		}
		return Vector3.one;
	}

}
