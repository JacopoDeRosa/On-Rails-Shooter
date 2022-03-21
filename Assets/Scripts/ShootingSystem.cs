using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
   
    [SerializeField] private HandCannon _leftArm;
    [SerializeField] private HandCannon _rightArm;
    [SerializeField] private float _trackingSpeed;
    [SerializeField] private float _zeroDistance;
    [SerializeField] private PlayerInput _input;

    private Vector2 _mousePosition;
    private Vector3 _targetPoint;
    private Vector3 _currentAimPoint;

    private void Start()
    {
        if (_input == null) _input = FindObjectOfType<PlayerInput>();
        _input.actions["Fire"].started += OnFireDown;
        _input.actions["Fire"].canceled += OnFireUp;
        _input.actions["Mouse Position"].performed += OnMousePosition;
    }
    private void OnDestroy()
    {
        if (_input == null) return;
        _input.actions["Fire"].started -= OnFireDown;
        _input.actions["Fire"].canceled -= OnFireUp;
        _input.actions["Mouse Position"].performed -= OnMousePosition;
    }
    private void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        _targetPoint = FindAimPoint();
       
    }
    private void OnFireDown(InputAction.CallbackContext context)
    {
        _leftArm.StartFiring();
        _rightArm.StartFiring();
    }
    private void OnFireUp(InputAction.CallbackContext context)
    {
        _leftArm.StopFiring();
        _rightArm.StopFiring();
    }
    private Vector3 FindAimPoint()
    {
        Vector3 aimPoint = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = ray.GetPoint(_zeroDistance);
        }
        return aimPoint;
    }

    private void Update()
    {
        if(_currentAimPoint != _targetPoint)
        {
            _currentAimPoint = Vector3.Lerp(_currentAimPoint, _targetPoint, _trackingSpeed * Time.deltaTime);
            _leftArm.transform.LookAt(_currentAimPoint);
            _rightArm.transform.LookAt(_currentAimPoint);
        }      
    }
}