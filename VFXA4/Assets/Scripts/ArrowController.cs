using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public GameObject playerArrowObject;
    public GameObject groundArrowObject;

    public bool isArrowOnGround = false;
    public bool toggleArrowVisibility = true;

    public ArrowScript arrowScript;
    
    void Start()
    {
        groundArrowObject.SetActive(true);
        playerArrowObject.SetActive(false);
        arrowScript.OnArrowGroundCollision.AddListener(ToggleArrow);
    }

    void Update()
    {
        if (toggleArrowVisibility)
        {
            groundArrowObject.SetActive(isArrowOnGround);
            playerArrowObject.SetActive(!isArrowOnGround);
        }
    }

    private void ToggleArrow()
    {
        isArrowOnGround = !isArrowOnGround;
    }
}