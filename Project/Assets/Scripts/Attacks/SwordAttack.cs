using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordAttack : AbstractAttack
{
    [SerializeField]
    GameObject swordPrefab;

    GameObject sword;
    public override void Attack()
    {
        if (quickTarget != null)
        {
            EventBus<DealDamageEvent>.Publish(new DealDamageEvent(CalculateDamage(), quickTarget, GetDefenseMultiplier()));
            EventBus<UseEnergyEvent>.Publish(new UseEnergyEvent(GetEnergyRequired()));
            swordVisuals();
        }

    }

    private void swordVisuals()
    {
        sword =  Instantiate(swordPrefab, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform.position, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform.rotation, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform);
    }




}
