using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int life = 100;
    Renderer ren;

    Color defalutColor;

    [SerializeField]
    Character character;
    void Awake()
    {
        EventBus<DealDamageEvent>.OnEvent += TakeDamage;
    }

    void OnDestroy()
    {
        EventBus<DealDamageEvent>.OnEvent -= TakeDamage;
    }
    void Start()
    {

        ren = GetComponent<Renderer>();
        if(ren == null)
        {
            ren = GetComponentInChildren<Renderer>();

        }
        if(character == null)
        {
            Debug.Log("Character missing ...");
        }

        defalutColor = ren.material.color;
    }

    void Update()
    {
        
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
            if (other.tag == "Weapon")
            {
                life -= 100;
                Debug.Log("attacked");
            }
    }
    


    void TakeDamage(DealDamageEvent dealDamageEvent)
    {
        if (dealDamageEvent.enemy ==this)
        {
            int newDmg = dealDamageEvent.damage - (int)(character.Defense *10 / (dealDamageEvent.defenseMultiplier +1));
            life -= newDmg;
            EventBus<ShowDamageFeedback>.Publish(new ShowDamageFeedback(newDmg, transform.position));
        }

    } 


    public void ChangeColor(Color color)
    {
        if (color == Color.white)
        {
            ren.material.color = defalutColor;
        }
        else
        {
            ren.material.color = color;
        }

    }
}
