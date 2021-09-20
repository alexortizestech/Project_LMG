using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Enemy", menuName ="Enemy")]
public class Enemy : ScriptableObject
{
    public float Walk;
   // public float RightLimit, LeftLimit;
    public string Name;
    public float Health;
    public string Damage;

    
}
