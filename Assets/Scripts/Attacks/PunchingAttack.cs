using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PunchingAttack : AbstractAttack
{
    [SerializeField]
    GameObject fistPrefab;

    GameObject fist;

    public override void Attack() 
    {
        if (quickTarget != null )
        {
            EventBus<DealDamageEvent>.Publish(new DealDamageEvent(CalculateDamage(), quickTarget, GetDefenseMultiplier()));
            EventBus<UseEnergyEvent>.Publish(new UseEnergyEvent(GetEnergyRequired()));
            punchVisuals();
        }

    }

    private void punchVisuals()
    {
        fist = Instantiate(fistPrefab, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform.position, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform.rotation, GameManager.Instance.GetPlayerController().GetWeaponHolder().transform);
    }
}
