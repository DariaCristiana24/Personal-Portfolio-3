using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed = 1.0f;
    Camera cam;

    AreaOfEffect aoe;

    [SerializeField]
    int explosionTime = 3;

    public int damage { private get;  set; }
    public float defenseMultiplier { private get; set; }

    void Start()
    {
        StartCoroutine(destroyTimer());
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        aoe = GetComponentInChildren<AreaOfEffect>();
        if (aoe == null)
        {
            Debug.Log("Aoe missing ... ");
        }
        if (rb == null)
        {
            Debug.Log("RigidBody missing ... ");
        }
        else
        {
            rb.AddForce ((Vector3.up + cam.transform.forward) * speed  );
            StartCoroutine(explosionTimer());
        }

    }

    IEnumerator explosionTimer()
    {
        yield return new WaitForSeconds( explosionTime );

        List<Enemy> enemies = aoe.GetEnemies();

        for (int i = 0; i < enemies.Count; i++)
        {
            EventBus<DealDamageEvent>.Publish(new DealDamageEvent(damage, enemies[i], defenseMultiplier));
        }

        Destroy( gameObject );
    }

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
}
