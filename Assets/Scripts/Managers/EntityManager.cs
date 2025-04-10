using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityManager : MonoBehaviour
{

    public List<CollidableObject> CollidableEntities = new List<CollidableObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CollidableEntities.RemoveAll(x => x == null || x.Equals(null));
    }

    public bool IsEntityPresent(Vector3Int position)
    {
        return CollidableEntities.Any(x => x.CurrentLocation == position);
    }

}
