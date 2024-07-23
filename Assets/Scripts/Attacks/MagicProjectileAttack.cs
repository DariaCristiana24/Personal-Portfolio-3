using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectileAttack : AbstractAttack
{
    [SerializeField]
    GameObject magicProjectilePrefab;

    private void Start()
    {
        if (magicProjectilePrefab == null) 
        {
            Debug.Log("Missing prefab projectile...");
        }
    }

    public override void Attack()
    {
        if (longTarget )
        {
            GameObject magicProjectile;
            magicProjectile = Instantiate(magicProjectilePrefab, GameManager.Instance.GetPlayerController().transform.position, Quaternion.identity);
            magicProjectile.GetComponent<ProjectileController>().damage = CalculateDamage();
            magicProjectile.GetComponent<ProjectileController>().defenseMultiplier = GetDefenseMultiplier();
            magicProjectile.GetComponent<ProjectileController>().target = longTarget;
            longTarget = null;
            EventBus<UseEnergyEvent>.Publish(new UseEnergyEvent(GetEnergyRequired()));
        }



    }
}
