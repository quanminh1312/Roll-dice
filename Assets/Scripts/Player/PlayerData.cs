using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [HideInInspector] public int healths = 4;
    [HideInInspector] public int shields = 2;
    [HideInInspector] public int potions = 0;
    [HideInInspector] public int Golds = 3;
    [HideInInspector] public int bombs = 0;
    [HideInInspector] public int skipTurns = 0;

    [HideInInspector] public int healthsMax = 4;
    [HideInInspector] public int shieldsMax = 3;

    public void Reset()
    {
        healths = 4;
        shields = 2;
        potions = 0;
        Golds = 3;
        bombs = 0;
        skipTurns = 0;
    }
}
