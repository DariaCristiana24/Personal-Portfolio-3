using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleProjectileAttack : AbstractAttack
{
    [SerializeField]
    GameObject arrowPrefab;

    private void Start ()
    {
        
        if (arrowPrefab == null)
        {
            Debug.Log("Missing prefab projectile...");
        }
    }

    public override void Attack()
    {

        float rotY = -30 ;
            GameObject[] arrows = new GameObject[3];

        Vector3 velocity = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);

            for (int i = 0; i < arrows.Length; i++)
            {
                arrows[i] = Instantiate(arrowPrefab, GameManager.Instance.GetPlayerController().transform.position, Quaternion.identity);
                arrows[i].GetComponent<ArrowController>().damage = CalculateDamage();
                arrows[i].GetComponent<ArrowController>().defenseMultiplier = GetDefenseMultiplier();
                arrows[i].GetComponent<ArrowController>().velocity = velocity;
           

                arrows[i].transform.Rotate(0, rotY, 0);
                rotY += 30;



            }
            EventBus<UseEnergyEvent>.Publish(new UseEnergyEvent(GetEnergyRequired()));
        




    }
}
