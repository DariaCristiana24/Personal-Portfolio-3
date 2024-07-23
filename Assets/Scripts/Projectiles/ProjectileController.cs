using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public int damage {private get;  set; }
    public float defenseMultiplier { private get; set; }
    public Enemy target { private get; set; }

    [SerializeField]
    float speed=1;
    void Start()
    {
        StartCoroutine(destroyTimer());
    }

    // Update is called once per frame
    void Update()
    {
        if (target!=null)
        {
            transform.Translate(Vector3.Normalize(target.transform.position + new Vector3(0, 1, 0) - transform.position) *speed * Time.deltaTime);
        }

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
