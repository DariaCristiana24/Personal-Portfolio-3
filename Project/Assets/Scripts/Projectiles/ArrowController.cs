using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArrowController : MonoBehaviour
{
    public Vector3 velocity { private get; set; }
    public int damage { private get; set; }
    public float defenseMultiplier { private get; set; }
    [SerializeField]
    float speed = 4;
    void Start()
    {
        StartCoroutine(destroyTimer());
    }

    void Update()
    {
         transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            EventBus<DealDamageEvent>.Publish(new DealDamageEvent(damage, other.GetComponent<Enemy>(), defenseMultiplier));
            Destroy(gameObject);
        }
    }

    IEnumerator destroyTimer()
    {
        yield return new WaitForSeconds(4);

        Destroy(gameObject);
    }
}
