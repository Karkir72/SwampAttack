using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightArrivalState : State
{
    [SerializeField] private float _rangeToTarget;
    [SerializeField] private float _height;
    [SerializeField] private float _speed;

    private Vector2 _startPosition;
    private Vector2 _endPosition;

    float currentTime;

    private void Start()
    {
        _startPosition = new Vector2(transform.position.x, transform.position.y +_height);
        _endPosition = new Vector2(Target.transform.position.x - _rangeToTarget, transform.position.y);
        currentTime = 0f;
    }

    void Update()
    {
        currentTime += Time.deltaTime * _speed;
        Vector2 currentPos = Vector2.Lerp(_startPosition, _endPosition, currentTime);
        currentPos.y += _height * Mathf.Sin(Mathf.Clamp01(currentTime) * Mathf.PI);
        transform.position = currentPos;
    }
}
