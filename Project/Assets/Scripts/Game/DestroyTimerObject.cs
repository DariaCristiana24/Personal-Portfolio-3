using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimerObject : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyObject(gameObject));
    }

    IEnumerator DestroyObject(GameObject g)
    {
        yield return new WaitForSeconds(1);
        Destroy(g);
    }
}
