using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    public int Damage;
    public int EnergyRequired;

    public float StrengthMultiplier;
    public float DefenseMultiplier;
    public float ManaMultiplier;
    public float AgilityMultiplier; 
    public float DexterityMultiplier;
}
