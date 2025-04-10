using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

[Serializable]
public class WallDefinition
{
    public string Name;

    public string N;

    public string E;

    public string S;

    public string W;

    public string NE;

    public string NS;

    public string NW;

    public string NES;

    public string NEW;

    public string NESW;

    public string NSW;

    public string ES; 

    public string ESW;

    public string EW;

    public string SW;

    public string Pillar; 
}


[Serializable]
public class WallCollection
{
    public List<WallDefinition> Walls;  
}

public class Walls
{
    public string Name;

    public TileBase  N;
            
    public TileBase  E;
            
    public TileBase  S;
            
    public TileBase  W;
            
    public TileBase  NE;
            
    public TileBase  NS;
            
    public TileBase  NW;
            
    public TileBase  NES;
            
    public TileBase  NEW;
            
    public TileBase  NESW;
            
    public TileBase  NSW;
            
    public TileBase  ES;
            
    public TileBase  ESW;
            
    public TileBase  EW;
            
    public TileBase  SW;

    public TileBase Pillar; 

    public TileBase GetTile(string directions)
    {
        switch (directions)
        {
            case "N":
                return N;
            case "E":
                return E;
            case "S":
                return S;
            case "W":
                return W;
            case "NE":
                return NE;
            case "NS":
                return NS;
            case "NW":
                return NW;
            case "NES":
                return NES;
            case "NEW":
                return NEW;
            case "NESW":
                return NESW;
            case "NSW":
                return NSW;
            case "ES":
                return ES;
            case "ESW":
                return ESW;
            case "EW":
                return EW;
            case "SW":
                return SW;

            default: 
                return Pillar;
        }
    }
}

