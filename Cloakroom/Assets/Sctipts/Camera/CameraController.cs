using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerBody;

    [Header("Settings")]
    [SerializeField] private float _cameraSensitivity = 100f;
    [SerializeField] private float _xRotation = 0f;

    private float _mouseX , _mouseY;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _mouseX = Input.GetAxis("Mouse X") * _cameraSensitivity * Time.deltaTime;
        _mouseY = Input.GetAxis("Mouse Y") * _cameraSensitivity * Time.deltaTime;

        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * _mouseX);
    }
}
