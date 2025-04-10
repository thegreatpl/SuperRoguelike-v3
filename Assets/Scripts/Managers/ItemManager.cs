using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public List<Weapon> Weapons; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator LoadItems()
    {
        Weapons = new List<Weapon>();

        var files = Resources.LoadAll<TextAsset>("Items");

        foreach (var file in files)
        {
            if (file.name == "weapons")
            {
                Weapons = JsonUtility.FromJson<WeaponCollection>(file.text).Weapons;
            }
        }

        yield return null;
    }

    public Weapon GetWeapon(string name)
    {
        return Weapons.FirstOrDefault(x => x.Name == name)?.Copy();
    }

    public List<Weapon> GetWeaponsOfType(string type)
    {
        var weapons = Weapons.Where(x => x.Type == type).ToList();
        List<Weapon> result = new List<Weapon>();
        foreach (var weapon in weapons)
        {
            result.Add(weapon.Copy());
        }
        return result;
    }
}
