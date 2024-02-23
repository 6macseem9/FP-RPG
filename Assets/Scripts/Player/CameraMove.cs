using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float SensX;
    public float SensY;

    [SerializeField] private Transform _orientation;

    private float _xRotation;
    private float _yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * SensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;

        _yRotation += mouseX;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
