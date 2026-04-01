using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _attackAction;
    private Animator _animator;

    public float attackTimerMax = 3.0f;
    private bool _isAttacking = false;
    private float _attackTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleAnimator();
        HandleAttacking();
    }

    private void HandleInput()
    {
        if (_attackAction.action.WasPressedThisFrame() && !_isAttacking)
        {
            _isAttacking = true;
        }
    }

    private void HandleAnimator()
    {
        _animator.SetBool("isAttacking", _isAttacking);
    }

    private void HandleAttacking()
    {
        if (!_isAttacking)
            return;

        _attackTimer += Time.deltaTime;

        if (_attackTimer >= attackTimerMax)
        {
            _isAttacking = false;
            _attackTimer = 0.0f;
        }
    }
}
