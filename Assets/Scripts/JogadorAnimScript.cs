using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorAnimScript : MonoBehaviour
{
    public Animator animatorController;
    public Transform playerTransform;

    [Space]
    public float sittingPositionX = 0;
    public float sittingPositionY = 0;
    public float sittingPositionZ = 0;

    private Vector3 _initialPlayerPosition;

    private bool isPushing = true;

    void Start()
    {
        _initialPlayerPosition = new Vector3(
            playerTransform.localPosition.x, 
            playerTransform.localPosition.y, 
            playerTransform.localPosition.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F)) 
        {
            isPushing = ChangePlayerState(isPushing);
        }
        animatorController.SetBool("pushing", isPushing);

    }

    public bool ChangePlayerState(bool statePushing) 
    {

        if (statePushing == false) 
        {
            playerTransform.SetLocalPositionAndRotation(_initialPlayerPosition, this.playerTransform.localRotation);
            Debug.Log(_initialPlayerPosition);
            return true;
        }
        else
        {
            playerTransform.localPosition = new Vector3(sittingPositionX, sittingPositionY, sittingPositionZ);
            return false;
        }
    }
}
