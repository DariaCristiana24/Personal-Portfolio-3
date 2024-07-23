using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.PackageManager;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    AbstractAttack[] attackers = new AbstractAttack[3];

    [SerializeField]
    Character character;


    Camera _camera;
    [SerializeField]
    int distanceRaycastMelee = 5;
    [SerializeField]
    int distanceRaycastRanged = 10;


    [SerializeField]
    Color colorShortDist;
    [SerializeField]
    Color colorLongDist;

    [SerializeField]
    LayerMask enemyMask;

    Enemy closestEnemy = null;
    RaycastHit oldHit;

    [SerializeField]
    float coolDown = 1;

    bool onCooldown = false;

    EnergySystem energySystem;

    int selectedAttack = 0;

    [SerializeField]
    GameObject weaponHolder;
    void Awake()
    {
        EventBus<SelectCharEvent>.OnEvent += ChooseChar;
    }

    void OnDestroy()
    {
        EventBus<SelectCharEvent>.OnEvent -= ChooseChar;
    }
    void Start()
    {
        _camera = FindObjectOfType<Camera>();
        energySystem = GetComponent<EnergySystem>();
        if (energySystem == null)
        {
            Debug.Log("Energy System mssng ...");
        }
        foreach (var attacker in attackers)
        {
            attacker.SetCharacterStats(character);
        }

        selectAttack(0);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectAttack(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectAttack(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectAttack(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectAttack(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selectAttack(4);
        }
        if (Input.GetMouseButtonDown(0))
        {
            doAttack(selectedAttack);
        }


        enemyRaycast(distanceRaycastMelee, false, colorShortDist); // look for nearby enemies
        enemyRaycast(distanceRaycastRanged, true, colorLongDist); //look for further away enemies


    }


   void selectAttack(int i)
    {
        selectedAttack = i;
        EventBus<UpdateActiveAttackEvent>.Publish(new UpdateActiveAttackEvent(attackers[i].name));
    }

    void doAttack(int index)
    {
        if (energySystem.CheckEnergy(attackers[index].GetEnergyRequired()))
        {
            if (!onCooldown)
            {
                if (closestEnemy)
                {
                    foreach (var attacker in attackers)
                    {
                        attacker.SetLongTarget(closestEnemy);
                    }
                }
                attackers[index].Attack();
                StartCoroutine(attackCooldown());

            }
        }
    }

    IEnumerator attackCooldown()
    {

        onCooldown = true;
        yield return new WaitForSeconds(coolDown);
        onCooldown = false;
    }

    void enemyRaycast(int distanceRaycast, bool longDistance, Color color)
    {
        Ray cameraRay = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (Physics.Raycast(cameraRay, out RaycastHit hitInfo, distanceRaycast, enemyMask))
        {
            if (closestEnemy == null) //select enemy if null
            {
                oldHit = hitInfo;
                closestEnemy = hitInfo.collider.GetComponent<Enemy>();
                foreach (var attacker in attackers)
                {
                    if (!longDistance)
                    {
                        attacker.SetQuickTarget(closestEnemy);
                    }
                    else
                    {
                        attacker.SetLongTarget(closestEnemy);
                    }
                }
                closestEnemy.ChangeColor(color);
            }
            else if (oldHit.distance > hitInfo.distance)//select closest enemy only if two are close to each other // prevents 2 enemies from being selected
            {
                foreach (var attacker in attackers)  //remove furthest enemy
                {
                    if (!longDistance)
                    {
                        attacker.SetQuickTarget(null);
                    }
                    else
                    {
                        attacker.SetLongTarget(null);
                    }
                }
                closestEnemy.ChangeColor(Color.white);

                oldHit = hitInfo;
                closestEnemy = hitInfo.collider.GetComponent<Enemy>();
                foreach (var attacker in attackers)  //select closest enemy
                {
                    if (!longDistance)
                    {
                        attacker.SetQuickTarget(closestEnemy);
                    }
                    else
                    {
                        attacker.SetLongTarget(closestEnemy);
                    }
                }
                closestEnemy.ChangeColor(color);
            }
        }
        else
        {
            if (closestEnemy != null)
            {
                foreach (var attacker in attackers)
                { 
                    attacker.SetQuickTarget(null); 
                }
                closestEnemy.ChangeColor(Color.white);
                closestEnemy = null;
            }
        }
    }

    void ChooseChar(SelectCharEvent selectCharEvent)
    {
        character = selectCharEvent.character;
    }

    public GameObject GetWeaponHolder()
    {
        return weaponHolder;
    }




}
