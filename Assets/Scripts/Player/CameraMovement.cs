using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    Transform orientation;
    [SerializeField]
    Transform playerModel; 

    float xAxis;
    float yAxis;

    [SerializeField]
    float sensitivity = 5;

    Quaternion cameraRotation;
    float maxRotation = 90;

    bool CursorUnlocked = false;
    void Awake()
    {
        EventBus<ChangeCursorStateEvent>.OnEvent += cursorStateChange;
    }

    void OnDestroy()
    {
        EventBus<ChangeCursorStateEvent>.OnEvent -= cursorStateChange;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!CursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        inputs();

        if (!CursorUnlocked)
        {
            cameraMovement();
        }

    }

    void cameraMovement()
    {
        cameraRotation.x += -yAxis * sensitivity;// * 100f;
        cameraRotation.y += xAxis * sensitivity; //* 100f;


        cameraRotation.x = Mathf.Clamp(cameraRotation.x, -maxRotation, maxRotation);
        firstPersonRotation();

    }

    void firstPersonRotation()
    {
        transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
        orientation.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        playerModel.transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
    }

    void inputs()
    {
        xAxis = Input.GetAxis("Mouse X");
        yAxis = Input.GetAxis("Mouse Y");    
    }

    private void cursorStateChange(ChangeCursorStateEvent changeCursorStateEvent)
    {

        if (!CursorUnlocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CursorUnlocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CursorUnlocked = false;
        }
    }

    public Transform GetOrientation()
    {
        return orientation;
    }
}
