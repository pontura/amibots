using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NesScripts.Controls.PathFind;

public class Tiles : MonoBehaviour {

	public List<Tile> tiles;
	public Tile tile;
	public SceneIngame sceneInGame;
	int tilesWidth = 14;
	int tilesWHeight = 6;
	float[,] tilesmap;
	public List<Point> path;
	NesScripts.Controls.PathFind.Grid grid;

	public void Init(int backgorundID) {
		Events.Blocktile += Blocktile;
		tilesmap = new float[tilesWidth, tilesWHeight];
		
		int id = 0;
		for (int a = 0; a < tilesWidth; a++) {
			for (int b = 0; b < tilesWHeight; b++) {

				bool isWalkable = false;
				tilesmap [a, b] = 1;
				isWalkable = true;
				Tile newTile = Instantiate (tile);
				newTile.transform.SetParent (sceneInGame.tilesContainer);
				newTile.Init(isWalkable, new Vector3 (a, 0, b));
				tiles.Add (newTile);
			}
		}		
		grid= new NesScripts.Controls.PathFind.Grid(tilesWidth, tilesWHeight, tilesmap);
        AddRandomObjects(backgorundID);
	}
    void AddRandomObjects(int backgorundID)
    {
        string[] randomObjects;
        if (backgorundID == 2)
        {
            randomObjects = new string[3];
            randomObjects[0] = "park_chair";
            randomObjects[1] = "park_popcorn";
            randomObjects[2] = "park_tree";
        }
        else
        {
            randomObjects = new string[2];
            randomObjects[0] = "space_chair";
            randomObjects[1] = "space_plant";
        }
       
        for (int a = 0; a < randomObjects.Length; a++)
        {
            SceneObjectData data = new SceneObjectData();
            data.sceneObjectName = randomObjects[a];
			int _x = Random.Range (0, 3);
			_x += 1;
			Vector2 pos = new Vector2(_x, (a*2));

            Events.AddGenericObject(data, GetTileByPos(pos).GetPos());
            Blocktile(GetTileByPos(pos), true);
        }
    }
	public void Blocktile(Tile tile, bool isBlock)
	{
		int value = 0;

		if (!isBlock) {
			tile.SetAsWalkable ();
			value = 1;
		} else {
			tile.SetAsUnwalkable ();
			value = 0;
		}
		tilesmap [(int)(tile.pos.x), (int)(tile.pos.y)] = value;
		grid= new NesScripts.Controls.PathFind.Grid(tilesWidth, tilesWHeight, tilesmap);

	}
	public List<Point> GetPathfinder(Vector3 _from , Vector3 _to )
	{		
		Point _f = new Point ((int)_from.x, (int)_from.z); 
		Point _t = new Point ((int)_to.x, (int)_to.z);
		List<Point> list = Pathfinding.FindPath(grid, _f, _t, true);
		foreach (Tile tile in tiles) {
			bool isOn = false;
			foreach (Point p in list) {
				if (Mathf.Round(p.x) == Mathf.Round(tile.pos.x) && Mathf.Round(p.y) == Mathf.Round(tile.pos.y)) {
					isOn = true;
					tile.MarkAsPath();
				}
			}
			if(!isOn)
				tile.ResetPath();
		}		
		return list;
	}
	public void BlocktileByPos(Vector2 pos, bool isBlocked)
	{
		Tile tile = GetTileByPos (pos);
		if(tile != null)
			Blocktile (tile, isBlocked);
	}
    public Tile GetTileByPos(Vector2 pos)
    {
        foreach (Tile tile in tiles)
			if (Mathf.Round( tile.pos.x) == Mathf.Round(pos.x) && Mathf.Round(tile.pos.y) == Mathf.Round(pos.y))
                return tile;

        return null;
    }
    public Vector3 GetPositionsByPoints(Point p)
    {
        foreach (Tile tile in tiles)
			if ( Mathf.Round(tile.pos.x) ==  Mathf.Round(p.x) &&  Mathf.Round(tile.pos.y) == Mathf.Round(p.y))
                return tile.transform.position;

        return Vector3.zero;
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
    public Tile GetTileByPosition(Vector3 pos)
    {
        foreach (Tile tile in tiles)
        {
			if ( Mathf.Round(tile.transform.localPosition.x) ==  Mathf.Round(pos.x) &&  Mathf.Round(tile.transform.localPosition.z) ==  Mathf.Round(pos.z))
                return tile;
        }
        return null;
    }

}
