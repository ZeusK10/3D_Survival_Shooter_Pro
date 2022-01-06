using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [Header("Controller Settings")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _jumpHeight = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    private Vector3 _velocity;
    private Vector3 _direction;

    private Camera _mainCamera;
    [Header("Camera Settings")]
    [SerializeField]
    private float _cameraSensitivity=1.0f;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if (_controller == null)
            Debug.LogError("Character Controller is NULL");

        _mainCamera = Camera.main;
        if (_mainCamera == null)
            Debug.LogError("Main Camera is NULL");

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CalculateMovement();
        CameraController();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void CameraController()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _cameraSensitivity;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        Vector3 currentCameraRotation = _mainCamera.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY*_cameraSensitivity;
        currentCameraRotation.x = Mathf.Clamp(currentCameraRotation.x, 0, 25);
        _mainCamera.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

    void CalculateMovement()
    {
        
        if (_controller.isGrounded)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            _direction = new Vector3(horizontal, 0, vertical);
            _velocity = _direction * _speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y = _jumpHeight;
            }
        }

        _velocity.y -= _gravity * Time.deltaTime;

        //Changes Local direction to world Direction
        _velocity = transform.TransformDirection(_velocity);

        _controller.Move(_velocity * Time.deltaTime);
    }
}
