using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static int Realm;
    public static long Health;
    public static long Strength;
    public static long Defense;

    public Player(int realm, long health, long strength, long defense)
    {
        Realm = realm;
        Health = health;
        Strength = strength;
        Defense = defense;
    }
}
