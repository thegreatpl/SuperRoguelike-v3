using System;
using UnityEngine;


public delegate void OnDeath();


public class EntityAttributes : MonoBehaviour
{
    public OnDeath OnDeath;

    public string Faction;

    public string Name;

    public int Level = 1;

    #region Variables

    public int Health; 

    public int MaxHealth;

    public int Stamina; 

    public int MaxStamina;

    #endregion

    #region Stats

    public int Strength;
    public int StrengthBonus { get { return GetBonus(Strength); } }

    public int Agility;
    public int AgilityBonus { get { return GetBonus(Agility); } }

    public int Constitution;
    public int ConstitutionBonus { get { return GetBonus(Constitution); } }

    public int Endurance; 
    public int EnduranceBonus { get { return GetBonus(Endurance); } }

    public int Intelligence;
    public int IntelligenceBonus { get { return GetBonus(Intelligence); } }

    public int Charisma;
    public int CharismaBonus { get { return GetBonus(Charisma); } }


    public EntityStats BaseStats;

    #endregion

    public int ToHit;

    public float MovementSpeed;

    public float VisionDistance;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RecalculatValues();

        Health = MaxHealth;

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


    public void RecalculatValues()
    {
        Strength = BaseStats.Strength;
        Agility = BaseStats.Agility;
        Endurance = BaseStats.Endurance;
        Constitution = BaseStats.Constitution;
        Intelligence = BaseStats.Intelligence;
        Charisma = BaseStats.Charisma;


        MaxHealth = Constitution * Level;

        ToHit = 10; 

        MovementSpeed = 100/Agility;

        VisionDistance = 10;
    }


    public static int GetBonus(int attribute)
    {
        return (attribute - 10) / 2;  
    }

}
