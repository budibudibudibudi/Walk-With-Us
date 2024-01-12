using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float MouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        Vector3 rot = new Vector3(0, x, 0);
        transform.Rotate(rot);
    }
}
