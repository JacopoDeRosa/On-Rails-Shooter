using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private Vector2 _mousePosition;
    [SerializeField] private ParticleSystem _bullets;
    [SerializeField] private float _zeroDistance;

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePosition = context.ReadValue<Vector2>();
    }
    
    public void OnShoot(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Vector3 aimPoint = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                aimPoint = hit.point;
            }
            else
            {
                aimPoint = ray.GetPoint(_zeroDistance);
            }
            _bullets.transform.LookAt(aimPoint);
            _bullets.Play();
        }
    }


  

}