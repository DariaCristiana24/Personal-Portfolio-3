using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    public int Strength;
    public int Defense;
    public int Mana;
    public int Agility;
    public int Dexterity;
}
