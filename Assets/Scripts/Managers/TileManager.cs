using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Dictionary<string, TileBase> Tiles = new Dictionary<string, TileBase>();


    public List<WallDefinition> WallDefs = new List<WallDefinition>();

    public Dictionary<string, Walls> Walls = new Dictionary<string, Walls>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CreateTiles()
    {
        Tiles.Clear();
        Walls.Clear();
        yield return null;
        int counter = 0;
        foreach (var sprite in GameManager.instance.spriteManager.Sprites)
        {
            var tilobj = ScriptableObject.CreateInstance<Tile>();
            tilobj.name = sprite.Name;
            tilobj.sprite = sprite.Sprite;
            tilobj.color = sprite.Color;
            Tiles.Add(tilobj.name, tilobj);


            counter++;
            if (counter > 10)
            {
                yield return null;
                counter = 0;
            }
        }
        yield return null;
        var files = Resources.LoadAll<TextAsset>("Walls");

        foreach (var file in files)
        {
            var wallsdefs = JsonUtility.FromJson<WallCollection>(file.text);
            foreach (var wall in wallsdefs.Walls)
            {
                WallDefs.Add(wall);
            }
        }

        yield return null;
        foreach (var wall in WallDefs)
        {
            var newWall = new Walls()
            {
                E = GetTile(wall.E),
                N = GetTile(wall.N),
                ES = GetTile(wall.ES),
                ESW = GetTile(wall.ESW), 
                EW = GetTile(wall.EW),
                Name = wall.Name,   
                NE = GetTile(wall.NE),
                NES = GetTile(wall. NES),
                NESW = GetTile(wall.NESW),
                NEW = GetTile(wall.NEW),
                NS = GetTile(wall.NS),
                NSW = GetTile(wall.NSW),
                NW = GetTile(wall.NW),
                S = GetTile(wall.S),
                SW = GetTile(wall.SW),
                W = GetTile(wall.W),
                Pillar = GetTile(wall.Pillar)
            };

            Walls.Add(newWall.Name, newWall);
        }

    }

    public TileBase GetTile(string name)
    {
        if (Tiles.ContainsKey(name))
            return Tiles[name];
        return null;
    }

    public Walls GetWalls(string name)
    {
        if (!Walls.ContainsKey(name)) return null;
        return Walls[name];
    }
}
