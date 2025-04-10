using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{

    public List<PrefabDefinition> Prefabs;

    public List<GameObject> PrefabsList;


    void Start()
    {
        foreach (var prefab in PrefabsList)
        {
            Prefabs.Add(new PrefabDefinition()
            {
                Name = prefab.name,
                Prefab = prefab
            }); 
        }
    }


    public GameObject GetPrefab(string prefabName)
    {
        return Prefabs.FirstOrDefault(x => x.Name == prefabName).Prefab;
    }
}
