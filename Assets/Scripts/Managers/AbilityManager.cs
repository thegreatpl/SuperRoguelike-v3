using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    public List<MeleeAttackAbility> MeleeAttackAbilities; 

    public Dictionary<string, BaseAbility> BaseAbilities;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadAbilities()
    {
        yield return null;

        foreach (var attackability in MeleeAttackAbilities)
        {
            BaseAbilities.Add(attackability.Name, attackability);
        }
    }


    public BaseAbility GetAbility(string name)
    {
        if (BaseAbilities.ContainsKey(name))
            return BaseAbilities[name];
        return null;
    }
}
