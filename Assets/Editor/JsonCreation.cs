using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class JsonCreation : MonoBehaviour
{
    [MenuItem("Json/CreateWeapons")]
    static void CreateWeaponJson()
    {
        var Weapons = new List<Weapon>
        {
            new Weapon()
            {
                Name = "Longsword",
                Type = "Londsword",
                DamageDice = Dice.d6,
                AttackSpeed = 10,
                DamageModifier = 0,
                DamageType = "slashing",
                Weight = 1
            },
            new Weapon()
            {
                Name = "Dagger",
                Type = "Dagger",
                DamageDice = Dice.d4,
                AttackSpeed = 10,
                DamageModifier = 0,
                DamageType = "slashing",
                Weight = 0.5f
            },
            new Weapon()
            {
                Name = "Fists",
                Type = "Fists",
                DamageDice = Dice.d2,
                AttackSpeed = 10,
                DamageModifier = 0,
                DamageType = "bludgeoning",
                Weight = 0.0f
            }
        };


        var json = EditorJsonUtility.ToJson(new WeaponCollection() { Weapons = Weapons}, true);

        File.WriteAllText($"Assets/Resources/Items/weapons.json", json);
    }


    [MenuItem("Json/CreateWallFile")]
    static void CreateWallFiles()
    {
        var walls = new List<WallDefinition>();

        walls.Add(CreateDefinition("stone"));
        walls.Add(CreateDefinition(""));


        var json = EditorJsonUtility.ToJson(new WallCollection() { Walls = walls }, true);

        File.WriteAllText($"Assets/Resources/Walls/walls.json", json);

    }


    static WallDefinition CreateDefinition(string name)
    {
        var definition = new WallDefinition()
        {
            Name = name,
            N = $"{name}wallvertical",
            S = $"{name}wallvertical",
            NS = $"{name}wallvertical",
            E = $"{name}wallhorizontal", 
            W = $"{name}wallhorizontal", 
            EW = $"{name}wallhorizontal", 
            ES = $"{name}walldownrightcorner", 
            ESW = $"{name}wallhorizontaldown", 
            NE = $"{name}walluprightcorner", 
            NES = $"{name}wallverticalright", 
            NESW = $"{name}wallcross", 
            NEW = $"{name}wallhorizontalup", 
            NSW = $"{name}wallverticalleft", 
            NW = $"{name}wallupleftcorner", 
            SW = $"{name}walldownleftcorner", 
            Pillar = $"{name}pillar"
        }; 

        return definition;
    }
}

