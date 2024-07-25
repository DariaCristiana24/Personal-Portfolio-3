using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnergySystem : MonoBehaviour
{
    [SerializeField]
    private int maxEnergy;

    [SerializeField]
    Slider energyBar;

    [SerializeField]
    int reloadPower = 5;

    public int currentEnergy { get; private set; }

    void Awake()
    {
        EventBus<UseEnergyEvent>.OnEvent += UseEnergy;
    }

    void OnDestroy()
    {
        EventBus<UseEnergyEvent>.OnEvent -= UseEnergy;
    }

    void Start()
    {
        currentEnergy = maxEnergy;
        StartCoroutine(reloadEnergy());
        if (energyBar)
        {
            energyBar.maxValue = maxEnergy;
            energyBar.value = currentEnergy;
        }
        else
        {
            Debug.Log("Slider missing ...");
        }
    }
    IEnumerator reloadEnergy()
    {   
        while (true)
        {
            if (maxEnergy > currentEnergy)
            {
                currentEnergy += reloadPower;
                if (energyBar)
                {
                    energyBar.value = currentEnergy;
                }
            }
            yield return new WaitForSeconds(1);

        }
    }

    private void UseEnergy(UseEnergyEvent useEnergyEvent)
    {
        currentEnergy -= useEnergyEvent.energy;
        if (energyBar)
        {
            energyBar.value = currentEnergy;
        }
    }

    public bool CheckEnergy(int e)
    {
        if(e > currentEnergy)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



}
