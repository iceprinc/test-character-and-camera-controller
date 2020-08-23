using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    private Vector3 Rot;
    private float xRot, yRot, xCurrRot, yCurrRot, xRotVelocity, yRotVelocity;
    public Camera fpsCamera;
    //public GameObject fpsObject;
    public float mouseSensetive, smoothDampTime;
    // Start is called before the first frame update
    void Start()
    {
        if (mouseSensetive == 0)
            mouseSensetive = 1.0f;
        if (smoothDampTime == 0)
            smoothDampTime = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
    }
    private void MouseMove()
    {
        xRot += Input.GetAxis("Mouse Y") * mouseSensetive;
        yRot += Input.GetAxis("Mouse X") * mouseSensetive;
        //xRot = Mathf.Clamp(xRot,-90,90);

        xCurrRot = Mathf.SmoothDamp(xCurrRot, xRot, ref xRotVelocity, smoothDampTime);
        yCurrRot = Mathf.SmoothDamp(yCurrRot,yRot,ref yRotVelocity,smoothDampTime);

        fpsCamera.transform.rotation = Quaternion.Euler(xCurrRot,yCurrRot,0f);
        //fpsObject.transform.rotation = Quaternion.Euler(0f, yCurrRot, 0f);
    }
}
