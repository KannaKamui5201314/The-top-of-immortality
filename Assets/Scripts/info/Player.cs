using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static int Realm;
    public static int Health;
    public static int Strength;
    public static int Defense;

    public Player(int realm,int health,int strength,int defense)
    {
        Realm = realm;
        Health = health;
        Strength = strength;
        Defense = defense;
    }
}
