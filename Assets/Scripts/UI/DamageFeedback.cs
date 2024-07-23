using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField]
    TextMeshPro moneyFeedback;
    // Start is called before the first frame update
    void Awake()
    {
        EventBus<ShowDamageFeedback>.OnEvent += showDealthDamage;
    }

    void OnDestroy()
    {
        EventBus<ShowDamageFeedback>.OnEvent -= showDealthDamage;
    }


    //Show a number next to the enemy with how much money was gained
    private void showDealthDamage(ShowDamageFeedback showDamageFeedback)
    {
        TextMeshPro text = Instantiate(moneyFeedback, showDamageFeedback.enemyPos + new Vector3(0, 1, -1), Quaternion.identity);
        text.SetText(showDamageFeedback.damage.ToString());
        text.transform.LookAt(Camera.main.transform);
        text.transform.Rotate(0,180, 0);

    }
}
