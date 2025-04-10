using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(EntityAttributes))]
public class EntityAbilities : MonoBehaviour
{
    public EntityAttributes Attributes;


    /// <summary>
    /// The default ability to do whenever running into a player. 
    /// </summary>
    public BaseAbility DefaultAbility; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Attributes = GetComponent<EntityAttributes>();

        DefaultAbility = new MeleeAttackAbility()
        {
            AttackDice = Dice.d2,
            BaseCooldown = 10,
            DamageModifier = 0,
            DamageType = "bludgening",
            Name = "Punch",
            StaminaCost = 1,
            ToHitModifier = 0
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public int UseMeleeAbility(MeleeAttackAbility meleeAttackAbility, Direction direction)
    {
        if (meleeAttackAbility == null)
        {
            return 0;
        }

        var realLoc = GameManager.instance.mapManager.GetWorldFromTile(GameManager.instance.mapManager.GetTileFromWorld(transform.position).GetInDirection(direction));
        var hits = Physics2D.OverlapPointAll(realLoc);
        bool hashit = false;
        foreach (var hit in hits)
        {
            var attribute = hit.attachedRigidbody?.gameObject?.GetComponent<EntityAttributes>();
            if (attribute != null && attribute.Faction != Attributes.Faction)
            {
                hashit = true;
                var attackroll = DiceRoller.RollDice(Dice.d20);
                if (attackroll == 1)
                {
                    GameManager.instance.ShowMessage($"{Attributes.Name} attacks {attribute.Name} but misses", Color.green);
                    continue; //crit fail. 
                }

                if (attackroll + meleeAttackAbility.ToHitModifier >= attribute.ToHit)
                {
                    var damage = DiceRoller.RollDice(meleeAttackAbility.AttackDice) + meleeAttackAbility.DamageModifier;
                    attribute.DealDamage(meleeAttackAbility.DamageType, damage, Attributes);
                }
                else
                {
                    GameManager.instance.ShowMessage($"{Attributes.Name} attacks {attribute.Name} but misses", Color.green);
                }

            }
        }
        if (hashit)
        {
            Attributes.Stamina -= meleeAttackAbility.StaminaCost;
            return meleeAttackAbility.BaseCooldown; //need to be a specific attack speed. 
        }
        return 0;
    }
}
