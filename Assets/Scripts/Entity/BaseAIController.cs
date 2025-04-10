using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class BaseAIController : BaseController
{

    public EntityAttributes Target; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterTickable();
        EntityAttributes.OnDeath += () => { Destroy(gameObject); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void RunTick()
    {
        if (Target == null)
        {
            var possibles = GetVisionObjects().ToList();
            if (possibles.Any())
            {
                Target = possibles.First();
            }

        }
        else
        {
            if (IsNextTo(Target))
            {
                AttackTarget(); 
            }
            else
            {
                DumbMoveToPosition(GameManager.instance.mapManager.GetTileFromWorld(Target.transform.position)); 
            }
        }
    }



    protected bool IsNextTo(EntityAttributes entityAttributes)
    {
        var entityPos = GameManager.instance.mapManager.GetTileFromWorld(entityAttributes.gameObject.transform.position);
        var thisPos = GameManager.instance.mapManager.GetTileFromWorld(transform.position);
        if (entityPos.GetNeighbours().Contains(thisPos))
            return true; 
        return false;
    }

    protected void DumbMoveToPosition(Vector3Int tile)
    {
        var currentPos = GameManager.instance.mapManager.GetTileFromWorld(transform.position); 

        if (currentPos == tile)
            return;

        var distance = tile - currentPos;

        if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
        {
            if (distance.x > 0)
            {
                Move(Direction.East); 
            }
            else
            {
                Move(Direction.West);
            }
        }
        else
        {
            if (distance.y < 0)
            {
                Move(Direction.South);
            }
            else
            {
                Move(Direction.North);
            }
        }
    }


    protected bool CanSee(GameObject other)
    {
        var hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, EntityAttributes.VisionDistance);
        if (hit.transform?.gameObject == other)
        { return true; }
        return false;
    }


    protected IEnumerable<EntityAttributes> GetVisionObjects()
    {
        var results = Physics2D.OverlapCircleAll(transform.position, EntityAttributes.VisionDistance); 

        foreach(var obj in results)
        {
            if (CanSee(obj.gameObject))
            {
                var attributes = obj.GetComponent<EntityAttributes>();
                if (attributes != null && attributes.Faction != EntityAttributes.Faction)
                    yield return attributes;
            }
        }
    }


    protected void AttackTarget()
    {
        var currentPos = GameManager.instance.mapManager.GetTileFromWorld(transform.position);
        var entityPos = GameManager.instance.mapManager.GetTileFromWorld(Target.gameObject.transform.position);

        if (currentPos.x < entityPos.x)
        {
            Attack(Direction.East);
        }
        else if (currentPos.x > entityPos.x)
        {
            Attack(Direction.West);
        }
        else if (currentPos.y < entityPos.y)
        {
            Attack(Direction.North);
        }
        else
        {
            Attack(Direction.South);
        }
    }


}
