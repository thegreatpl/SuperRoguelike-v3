using UnityEngine;
using UnityEngine.Tilemaps;

//[RequireComponent(typeof(MapGenerator))]
public class MapManager : MonoBehaviour
{

    public Tilemap Ground; 

    public Tilemap Walls;

    //public MapGenerator MapGenerator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       // MapGenerator = GetComponent<MapGenerator>();
    }

    public bool IsPassable(Vector3Int Location)
    {
        if (Walls.GetTile(Location) != null) 
            return false;

        return true;
    }

    public Vector3Int GetTileFromWorld(Vector3 postion)
    {
        return Ground.WorldToCell(postion);
    }

    public Vector3 GetWorldFromTile(Vector3Int Tile)
    {
        return Ground.GetCellCenterWorld(Tile);
    }



}
