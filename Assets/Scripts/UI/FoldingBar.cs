using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FoldingBar : MonoBehaviour
{
    [SerializeField] private Directions _direction;
    [SerializeField] private float _sinkAmount;
    [SerializeField] private float _sinkSpeed = 1;
    [SerializeField] private CanvasScaler _scaler;

    private bool _busy;
    private bool _open;

    public bool Open { get => _open; }


    private void Awake()
    {
        _open = true;
    }

    private void Start()
    {
        _sinkAmount *= FindScale(); 
    }

    private float FindScale()
    {
       return Screen.width / _scaler.referenceResolution.x;
    }


    public void Toggle(bool status)
    {
        if (_busy) return;
        if(status == true && _open == false)
        {
            _open = true;
          StartCoroutine(MoveToPosition(0));

        }
        else if(status == false && _open)
        {
            _open = false;
            StartCoroutine(MoveToPosition(_sinkAmount));
        }
    }

    
    public IEnumerator Toggle()
    {
        if (_busy) yield break;

        if (_open == false)
        {
            _open = true;
            yield return MoveToPosition(0);
        }
        else if (_open)
        {
            _open = false;
            yield return MoveToPosition(_sinkAmount);
        }
    }
    private IEnumerator MoveToPosition(float position)
    {
        _busy = true;
        float t = 0;
        Vector3 start = transform.position;
        Vector3 end = Vector3.zero;

        int open = -1;
        if(_open == false)
        {
            open = 1;
        }

        switch (_direction)
        {
            case Directions.Top:
                end = new Vector3(start.x, start.y + _sinkAmount * open, start.z);
                break;

            case Directions.Right:
                end = new Vector3(start.x + _sinkAmount * open, start.y, start.z);
                break;

            case Directions.Bottom:
                end = new Vector3(start.x, start.y - _sinkAmount * open, start.z);
                break;

            case Directions.Left:
                end = new Vector3(start.x - _sinkAmount * open, start.y, start.z);
                break;
        }

 
        // Declaring wait once reduces garbage
        WaitForFixedUpdate wait = new WaitForFixedUpdate();  

        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, end, t);
            t += Time.fixedDeltaTime * _sinkSpeed;
            yield return wait;
        }

        _busy = false;
    }
}
