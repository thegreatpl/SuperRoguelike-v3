using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random; 

[Serializable]
public enum Dice
{
    d20 = 20, 
    d2 = 2,
    d4 = 4, 
    d6 = 6, 
    d8 = 8, 
    d10 = 10, 
    d12 = 12, 
    d100 = 100
}

public static class DiceRoller
{

    public static int RollDice(Dice dice)
    {
        switch (dice)
        {
            case Dice.d20:
                return RollDice(20); 
            case Dice.d4:
                return RollDice(4);
            case Dice.d6:
                return RollDice(6);
            case Dice.d8:
                return RollDice(8);
            case Dice.d10:
                return RollDice(10);
            case Dice.d12:
                return RollDice(12);
            case Dice.d100:
                return RollDice(20);
            case Dice.d2:
                return RollDice(2);
        }
        return 0;
    }


    public static int RollDice(int size)
    {
        return Random.Range(0, size);
    }


}

