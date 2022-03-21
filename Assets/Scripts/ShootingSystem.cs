using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Vector2 _mousePosition;
    [SerializeField] private HandCannon _leftArm;
    [SerializeField] private HandCannon _rightArm;
    [SerializeField] private ParticleSystem[] _bullets;
    [SerializeField] private float _zeroDistance;
    [SerializeField] private PlayerInput _input;

    private void Start()
    {
        if (_input == null) _input = FindObjectOfType<PlayerInput>();
        _input.actions["Fire"].started += OnFireDown;
        _input.actions["Fire"].canceled += OnFireUp;

        _input.actions["Mouse Position"].performed += OnMousePosition;
    }



    private void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
        Vector3 aimPoint = FindAimPoint();
        _leftArm.transform.LookAt(aimPoint);
        _rightArm.transform.LookAt(aimPoint);
       
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
  

}