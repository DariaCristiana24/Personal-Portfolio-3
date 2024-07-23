using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfEffect : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemiesInArea = new List<Enemy>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i<enemiesInArea.Count;i++) 
        {
            if (enemiesInArea[i] == null)
            {
                enemiesInArea.Remove(enemiesInArea[i]);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemy e = other.GetComponent<Enemy>();
            enemiesInArea.Add(e);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            Enemy e = other.GetComponent<Enemy>();
            enemiesInArea.Remove(e);
        }
    }

    public List<Enemy> GetEnemies()
    {
        return enemiesInArea;
    }
}
