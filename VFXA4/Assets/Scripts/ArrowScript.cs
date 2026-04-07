using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArrowScript : MonoBehaviour
{
    public UnityEvent OnArrowGroundCollision;
    
    void Start()
    {
        OnArrowGroundCollision ??= new UnityEvent();
    }

    void Update()
    {
    }

    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            OnArrowGroundCollision.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            OnArrowGroundCollision.Invoke();
        }
    }
}
