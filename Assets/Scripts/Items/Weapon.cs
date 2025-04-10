using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class Weapon  : IItem
{
    public string Name;

    public string Type;
    public string Description;

    public Dice DamageDice;

    public int DamageModifier;

    public string DamageType;

    public int AttackSpeed;

    public float Weight;

    float IItem.Weight { get { return Weight; } }

    public Weapon Copy()
    {
        return new Weapon()
        {
            Name = Name,
            DamageDice = DamageDice,
            DamageModifier = DamageModifier,
            DamageType = DamageType,
            Description = Description,
            Type = Type, 
            Weight = Weight, 
            AttackSpeed = AttackSpeed
        };
    }
}

[Serializable]
public class WeaponCollection
{
    public List<Weapon> Weapons;
}

