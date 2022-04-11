using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _maxEnemiesHits;
    [SerializeField] private int _powerFadeFactor;
    [SerializeField] private float _speed;

    private int _currentDamage;

    private void Start()
    {
        _currentDamage = _damage;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
            _maxEnemiesHits--;
            _currentDamage = _currentDamage / _powerFadeFactor;
        }

        if (_maxEnemiesHits == 0)
            Destroy(gameObject);
    }
}
