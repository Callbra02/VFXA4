using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _attackAction;

    [SerializeField] private ParticleSystem _flareParticleSystem;
    [SerializeField] private ParticleSystem _arrowParticleSystem;
    
    private Animator _animator;

    public float attackTimerMax = 3.0f;
    public float vfxTimerMax = 2.0f;
    
    private bool _isAttacking = false;
    private bool _isPlayingVFX = false;
    
    private float _attackTimer = 0.0f;
    private float _vfxTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _flareParticleSystem.Stop();
        _arrowParticleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleAnimator();
        HandleVFX();
        HandleAttacking();
    }

    private void HandleInput()
    {
        if (_attackAction.action.WasPressedThisFrame() && !_isAttacking)
        {
            _isPlayingVFX = true;
            _isAttacking = true;
        }
    }

    private void HandleAnimator()
    {
        _animator.SetBool("isAttacking", _isAttacking);
    }

    private void HandleVFX()
    {
        if (!_isPlayingVFX && _arrowParticleSystem.isPlaying)
        {
            _arrowParticleSystem.Stop();
            _flareParticleSystem.Stop();
        }
        
        if (_isPlayingVFX && !_arrowParticleSystem.isPlaying)
        {
            _arrowParticleSystem.Play();
            _flareParticleSystem.Play();
        }

        if (!_isPlayingVFX)
        {
            return;
        }

        _vfxTimer += Time.deltaTime;

        if (_vfxTimer >= vfxTimerMax)
        {
            _isPlayingVFX = false;
            _vfxTimer = 0.0f;
        }
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
