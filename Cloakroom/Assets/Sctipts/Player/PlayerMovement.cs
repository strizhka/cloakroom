using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _cameraTransform;

    [Header("Settings")]
    [SerializeField] private float _speed = 10f;

    private float _inputX, _inputZ;
    private Vector3 _movementDirection;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _inputX = Input.GetAxis("Horizontal");
        _inputZ = Input.GetAxis("Vertical");

        Vector3 forward = _cameraTransform.forward;
        Vector3 right = _cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        _movementDirection = right * _inputX + forward * _inputZ;

        _characterController.Move(_movementDirection * _speed * Time.deltaTime);
    }
}
