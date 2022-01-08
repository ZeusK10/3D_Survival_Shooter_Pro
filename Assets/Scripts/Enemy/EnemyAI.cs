using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    CharacterController _controller;

    private Transform player;
    private Health _playerHealth;

    private Vector3 _direction;
    private Vector3 _velocity;
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private float _gravity = 20.0f;
    private EnemyState currentState = EnemyState.Chase;

    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1f;


    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        if(_controller==null)
        {
            Debug.LogError("Character Controller is NULL");
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(player==null)
        {
            Debug.LogError("Player script not found!!!");
        }

        _playerHealth = player.GetComponent<Health>();
        if(_playerHealth==null)
        {
            Debug.LogError("Player Health is NULL");
        }
    }

    private void Update()
    {
        switch(currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Chase:
                CalculateMovement();
                break;
        }
    }

    void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Damage(10);
            }

            _nextAttack = Time.time + _attackDelay;
        }
    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded)
        {
            _direction = player.position - transform.position;
            _direction.Normalize();
            _direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(_direction);
            _velocity = _direction * _speed;
        }

        _velocity.y -= _gravity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void StartAttack()
    {
        currentState = EnemyState.Attack;
    }

    public void StopAttack()
    {
        currentState = EnemyState.Chase;
    }

}
