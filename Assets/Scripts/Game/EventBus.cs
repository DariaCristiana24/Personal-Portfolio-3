using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class Event { }

//The event bus 
public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public class DealDamageEvent : Event 
{
    public DealDamageEvent(int pDamage, Enemy pEnemy, float pDefense )
    {
        damage = pDamage;
        enemy = pEnemy;
        defenseMultiplier = pDefense;
    }

    public Enemy enemy;
    public int damage;
    public float defenseMultiplier;
}
public class ShowDamageFeedback : Event 
{
    public ShowDamageFeedback(int pDamage, Vector3 pEnemyPos)
    {
        damage = pDamage;
        enemyPos = pEnemyPos;
    }

    public int damage;
    public Vector3 enemyPos;
}

public class UseEnergyEvent : Event
{
    public UseEnergyEvent(int pEnergy)
    {
        energy = pEnergy;
    }

    public int energy;
}

public class SelectCharEvent : Event
{
    public SelectCharEvent(Character pCharacter)
    {
        character = pCharacter;
    }

    public Character character;
}

public class ChangeCursorStateEvent : Event
{
    public ChangeCursorStateEvent()
    {
    }
}

public class UpdateActiveAttackEvent : Event
{
    public UpdateActiveAttackEvent(string pAttackName)
    {
        attackName = pAttackName;
    }
    public string attackName;
}





