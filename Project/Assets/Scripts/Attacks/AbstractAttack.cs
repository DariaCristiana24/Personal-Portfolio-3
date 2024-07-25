using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    [SerializeField]
    Attack attack;
    Character character;

    [HideInInspector]
    public Enemy quickTarget = null;
    [HideInInspector]
    public Enemy longTarget = null;

    public void SetCharacterStats(Character pCharacter)
    {
        character = pCharacter;
    }

    public void SetQuickTarget(Enemy pTarget)
    {
        quickTarget = pTarget;
    }
    public void SetLongTarget(Enemy pTarget)
    {
        longTarget = pTarget;
    }

    public abstract void Attack();

    public int CalculateDamage()
    {
        if(character == null)
        {
            Debug.Log("Character Stats missing ...");
            return 0;
        }
        if (attack == null)
        {
            Debug.Log("Attack Stats missing ...");
            return 0;
        }

        float ran = Random.Range(1, 1.5f);

        float damage =  attack.Damage * (attack.StrengthMultiplier * character.Strength + attack.DefenseMultiplier * character.Defense + attack.ManaMultiplier * character.Mana + attack.AgilityMultiplier * character.Agility + attack.DexterityMultiplier * character.Dexterity) * ran;   
        return (int)damage;
    }

    public float GetDefenseMultiplier()
    {
        return attack.DefenseMultiplier;
    }
    public int GetEnergyRequired()
    {
        return attack.EnergyRequired;
    }

}
