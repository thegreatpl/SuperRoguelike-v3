using System;
using UnityEngine;

public enum AbilityType
{
    MeleeAttack, 
    RangedAttack
}

[Serializable]
public abstract class BaseAbility 
{
    public string Name; 

    public string Description;

    public virtual AbilityType Type { get; }

}
