using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour, IUnit
{
    [SerializeField] private float _currentHp;
    public readonly float _maxHp = 100;

    [Header("Movement")]
    [SerializeField] private float _currentSpeed;
    [SerializeField] private float _tempSpeed;

    public Transform _orientation;

    [SerializeField] private Transform _startPoint;

    float _horizontalInput;
    float _verticalInput;

    Vector3 _moveDirection;

    Rigidbody _rb;

    public static Action<float, float> OnHealthChanged;
    public static Action DeleteAllActiveElements;

    public bool ShieldExists { get; set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MyInput();
        SpeedControl();
        CursorControl();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void CursorControl() {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        _rb.AddForce(_moveDirection.normalized * _currentSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

        if (flatVel.magnitude > _currentSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _currentSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    public void AddHP(float hp)
    {
        _currentHp = Mathf.Clamp(_currentHp + hp, 0, _maxHp);
        OnHealthChanged?.Invoke(_currentHp, _maxHp);
    }

    public void TakeDamage(float damage) {
        if (ShieldExists)
            ShieldExists = false;
        else
        {
            _currentHp -= damage;
            OnHealthChanged?.Invoke(_currentHp, _maxHp);

            if (_currentHp <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        Destroy(gameObject);

        DeleteAllActiveElements?.Invoke();
    }

    public void SpeedChange(float speed)
    {
        _tempSpeed = _currentSpeed + speed;

        if (_tempSpeed >= 0)
            StartCoroutine("TempSpeedChange");
    }

    IEnumerator TempSpeedChange()
    {
        float prevSpeed = _currentSpeed;
        _currentSpeed = _tempSpeed;
        yield return new WaitForSeconds(7);
        _currentSpeed = prevSpeed;
    }

    public void Respawn()
    {
        Vector3 position = _startPoint.position;
        position.y = 1;
        _rb.MovePosition(position);
    }
}
