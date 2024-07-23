using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicExplosiveAttack : AbstractAttack
{
    [SerializeField]
    GameObject MagicExplosivePrefab;

    public override void Attack()
    {


            GameObject magicProjectile = Instantiate(MagicExplosivePrefab, GameManager.Instance.GetPlayerController().transform.position + transform.forward, Quaternion.identity);

            magicProjectile.GetComponent<ExplosiveController>().damage = CalculateDamage();
            magicProjectile.GetComponent<ExplosiveController>().defenseMultiplier = GetDefenseMultiplier();

            EventBus<UseEnergyEvent>.Publish(new UseEnergyEvent(GetEnergyRequired()));
        

    }
}
