using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;
    private Vector3 _targetPos;

    private Vector3 _currentPos;

    [SerializeField]
    private float _yDistance = 2.25f;
    [SerializeField]
    private float _zDistance = 8f;

    [SerializeField]
    private float _cameraSpeed;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        _currentPos = transform.position;
        
        _targetPos = _target.transform.position;
        _targetPos.y += _yDistance;
        _targetPos.z -= _zDistance;

        transform.position = Vector3.Lerp(_currentPos, _targetPos, _cameraSpeed * Time.deltaTime);
    }
}
