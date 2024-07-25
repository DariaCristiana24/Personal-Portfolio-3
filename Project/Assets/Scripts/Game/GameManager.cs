using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    PlayerController playerController;

    DestroyTimerObject destroyTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        destroyTimer = GetComponent<DestroyTimerObject>();
        if (playerController == null)
        {
            Debug.Log("Controller missing...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }


}
