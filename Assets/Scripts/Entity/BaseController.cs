using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EntityAttributes))]
public class BaseController : MonoBehaviour, ITickable
{
    public int Cooldown { get { return coolDown; } }


    public int coolDown;

    public bool WaitingForPlayerInput { get { return waitingForPlayerinput; } }

    public bool waitingForPlayerinput = false;


    public bool IsPlayerControlled = false;



    public EntityAttributes EntityAttributes; 



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EntityAttributes = GetComponent<EntityAttributes>();
        RegisterTickable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void RegisterTickable()
    {
        EntityAttributes = GetComponent<EntityAttributes>();
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        yield return null;
        GameManager.instance.timeManager.TimeObjects.Add(this);
        var collide = GetComponent<CollidableObject>();
        if (collide != null)
        {
            GameManager.instance.entityManager.CollidableEntities.Add(collide);
        }
    }

    public virtual void EndTick()
    {
        if (coolDown > 0) 
            coolDown--;

        if (IsPlayerControlled )
            waitingForPlayerinput = true;
    }

    public virtual void RunTick()
    {
        
    }


    public virtual void Move(Direction direction)
    {
        var map = GameManager.instance.mapManager;

        var current = map.GetTileFromWorld(transform.position);
        var tileToMoveTo = current.GetInDirection(direction);

        if (!map.IsPassable(tileToMoveTo) || GameManager.instance.entityManager.IsEntityPresent(tileToMoveTo))
            return; 

        transform.position = map.GetWorldFromTile(tileToMoveTo);

        coolDown = (int)EntityAttributes.MovementSpeed;
    }

    public virtual void Attack(Direction direction)
    {
        Attack(GameManager.instance.mapManager.GetTileFromWorld(transform.position).GetInDirection(direction)); 
    }

    public virtual void Attack(Vector3Int location)
    {
        var realLoc = GameManager.instance.mapManager.GetWorldFromTile(location);
        var hits = Physics2D.OverlapPointAll(realLoc);
        bool hashit = false; 
        foreach ( var hit in hits )
        {
            var attribute = hit.attachedRigidbody?.gameObject?.GetComponent<EntityAttributes>();
            if ( attribute != null && attribute.Faction != EntityAttributes.Faction)
            {
                hashit = true; 
                var attackroll = DiceRoller.RollDice(Dice.d20);
                if (attackroll == 1)
                {
                    GameManager.instance.ShowMessage($"{EntityAttributes.Name} attacks {attribute.Name} but misses", Color.green); 
                    continue; //crit fail. 
                }

                if (attackroll >= attribute.ArmourClass)
                {
                    var damage = EntityAttributes.AttackDamage;
                    attribute.DealDamage(EntityAttributes.DamageType, damage, EntityAttributes);
                }
                else
                {
                    GameManager.instance.ShowMessage($"{EntityAttributes.Name} attacks {attribute.Name} but misses", Color.green);
                }

            }
        }
        if (hashit)
        {
            coolDown = (int)EntityAttributes.AttackSpeed; //need to be a specific attack speed. 
        }

    }

    public void BeginTick()
    {
    }
}
