using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class MeleeAttackAbility : BaseAbility
{
    public override AbilityType Type => AbilityType.MeleeAttack;

    public string DamageType;

    public int DamageModifier;

    public Dice AttackDice;

    public int BaseCooldown;

    public int ToHitModifier;

    public int StaminaCost; 
}

