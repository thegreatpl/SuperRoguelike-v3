using System;
using UnityEngine;


public delegate void OnDeath();


public class EntityAttributes : MonoBehaviour
{
    public OnDeath OnDeath;

    public string Faction;

    public string Name;

    public int Level = 1; 


    public int Health; 

    public int MaxHealth;


    public int Strength;

    public int Dexterity;

    public int Constitution;

    public int Intelligence;

    public int Wisdom; 




    public int ArmourClass
    {
        get
        {
            return 10; //todo: add in the armour check here. 
        }
    }

    public float MovementSpeed
    {
        get
        {
            return 100 / Dexterity; 
        }
    }

    public float VisionDistance
    {
        get {
            return Wisdom; 
        }
    }

    public float AttackSpeed
    {
        get
        {
            if (EquippedWeapon != null)
            {
                return EquippedWeapon.AttackSpeed;
            }
            else
            {
                return 100 / Dexterity;
            }
        }
    }

    public string DamageType
    {
        get
        {
            if (EquippedWeapon != null)
            {
                return EquippedWeapon.DamageType;
            }
            else
            {
                return "bludgeoning"; //fists
            }
        }
    }

    public int AttackDamage
    {
        get
        {
            if (EquippedWeapon != null)
            {
                var damage = DiceRoller.RollDice(EquippedWeapon.DamageDice) + EquippedWeapon.DamageModifier + GetBonus(Strength);
                if (damage < 1)
                {
                    damage = 1;
                }

                return damage;
            }
            else
            {
                var damage = DiceRoller.RollDice(2) + GetBonus(Strength); //fists
                if (damage < 1)
                {
                    damage = 1;
                }
                return damage;
            }
        }
    }



    public Weapon EquippedWeapon; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MaxHealth = Constitution * Level;

        Health = MaxHealth;
        EquippedWeapon = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (Health < 0)
        {
            Death();
            return;
        }
    }

    public void Death()
    {
        OnDeath?.Invoke();
    }

    public void DealDamage(string attackType, int damage, EntityAttributes attackerAttributes)
    {
        //insert things like resistances here. 
        GameManager.instance.ShowMessage($"{attackerAttributes.Name} attacks {Name} and deals {damage}.", Color.red);


        Health -= damage;
        if (Health < 0)
        {
            Death();
            GameManager.instance.ShowMessage($"{attackerAttributes.Name} has killed {Name}!", Color.red);
        }
    }


    public int GetBonus(int attribute)
    {
        return (attribute - 10) / 2;  
    }

}
